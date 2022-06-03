namespace CreditConsult.Data.Seeding.SeedScheduleHosted;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CreditConsult.Data.Data;
using CreditConsult.Data.Models;
using CreditConsult.Data.Seeding.SeedScheduleHosted.Interfaces;

public class ScopedProcessingService : IScopedProcessingService
{
    private readonly ApplicationDbContext dbContext;

    private readonly List<string> daysOfWeek = new()
    {
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sunday",
    };

    private readonly IList<string> hours = new List<string>
    {
            "09:00",
            "09:30",
            "10:00",
            "10:30",
            "11:00",
            "11:30",
            "12:00",
            "12:30",
            "13:30",
            "14:00",
            "14:30",
            "15:00",
            "15:30",
            "16:00",
            "16:30",
    };

    private int rotateBeforeStart = 0;
    private int daysLimit = DateTime.DaysInMonth(2022, DateTime.UtcNow.Month + 1);

    public ScopedProcessingService(ApplicationDbContext dbContext)
        => this.dbContext = dbContext;

    public async Task SeedNextMonth(CancellationToken cancellationToken)
    {
        var isSeededThisMonth = this.dbContext.AppointmentsForDays
            .ToList()
            .Any(d => int.Parse(d.Date!.Split(new char[] { '/', ' ' })[1]) == DateTime.UtcNow.Month);

        var isSeededNextMonth = this.dbContext.AppointmentsForDays
            .ToList()
            .Any(d => int.Parse(d.Date!.Split(new char[] { '/', ' ' })[1]) == DateTime.UtcNow.Month + 1);

        var isDatabaseCreated = this.dbContext.AppointmentsForDays.Any();

        while (!cancellationToken.IsCancellationRequested)
        {
            if (!isSeededThisMonth)
            {
                this.SeedMonthlyAppointmentsSchedule(DateTime.UtcNow.Month);
            }

            if (DateTime.UtcNow.Day >= 14 && isDatabaseCreated && !isSeededNextMonth)
            {
                this.SeedMonthlyAppointmentsSchedule(DateTime.UtcNow.Month + 1);
            }

            await Task.Delay(1000 * 60 * 60 * 24, cancellationToken);
        }
    }

    public void SeedMonthlyAppointmentsSchedule(int monthNumber)
    {
        var users = this.dbContext.Users.Where(u => u.IsEmployee).ToList();
        var daysTillEndOfMonth = this.AllDates(monthNumber);

        foreach (var user in users)
        {
            var allAppsForDate = new HashSet<AppointmentsForDay>();

            foreach (var day in daysTillEndOfMonth)
            {
                var hoursForDay = new List<HourForAppontment>();

                var appointmentsForDay = new AppointmentsForDay
                {
                    ApplicationUserId = user.Id,
                    Date = day,
                };

                this.dbContext.AppointmentsForDays.Add(appointmentsForDay);

                this.dbContext.SaveChanges();

                foreach (var hour in this.hours)
                {
                    var appointmentHour = new HourForAppontment
                    {
                        Time = hour,
                        DailyAppointmentsId = appointmentsForDay.Id,
                    };

                    this.dbContext.HourForAppontments.Add(appointmentHour);

                    this.dbContext.SaveChanges();

                    hoursForDay.Add(appointmentHour);
                }

                this.dbContext.SaveChanges();

                allAppsForDate.Add(appointmentsForDay);

                this.dbContext.SaveChanges();
            }

            this.dbContext.SaveChanges();
        }
    }

    private IEnumerable<string> AllDates(int monthNumber)
    {
        var date = 0;
        var month = (monthNumber).ToString();

        var day = this.GetFirstDayNextMonth(monthNumber);

        this.rotateBeforeStart = CheckRotations(day);

        for (var i = 0; i < this.rotateBeforeStart; i++)
        {
            var dayToTurn = this.daysOfWeek[0];
            this.daysOfWeek.RemoveAt(0);

            this.daysOfWeek.Add(dayToTurn);
        }

        var listOfDates = new List<string>();

        this.daysLimit = this.GetDaysLimit(month);

        day = this.daysOfWeek[0];
        this.daysOfWeek.RemoveAt(0);

        this.daysOfWeek.Add(day);

        for (int i = date + 1; i <= this.daysLimit; i++)
        {
            if (day == "Saturday" || day == "Sunday")
            {
                day = this.daysOfWeek[0];
                this.daysOfWeek.RemoveAt(0);

                this.daysOfWeek.Add(day);
                continue;
            }

            listOfDates.Add($"{i}/{month} - {day}");

            day = this.daysOfWeek[0];
            this.daysOfWeek.RemoveAt(0);

            this.daysOfWeek.Add(day);
        }

        return listOfDates;
    }

    private string GetFirstDayNextMonth(int month)
        => month switch
        {
            1 => "Saturday",
            2 => "Tuesday",
            3 => "Tuesday",
            4 => "Friday",
            5 => "Sunday",
            6 => "Wednesday",
            7 => "Friday",
            8 => "Monday",
            9 => "Thursday",
            10 => "Saturday",
            11 => "Tuesday",
            12 => "Thursday",
            _ => throw new ArgumentException(month.ToString(), "Invalid month"),
        };

    private int GetDaysLimit(string month)
        => month switch
        {
            var name when
                name == "1" ||
                name == "3" ||
                name == "5" ||
                name == "7" ||
                name == "8" ||
                name == "10" ||
                name == "12" => this.daysLimit = 31,
            var name when
                name == "4" ||
                name == "6" ||
                name == "9" ||
                name == "11" => this.daysLimit = 30,
            _ => throw new ArgumentException(month, "Invalid month"),
        };

    private static int CheckRotations(string startDay)
        => startDay switch
        {
            "Monday" => 1,
            "Tuesday" => 2,
            "Wednesday" => 3,
            "Thursday" => 4,
            "Friday" => 5,
            "Saturday" => 6,
            "Sunday" => 7,
            _ => throw new ArgumentException(startDay, "Invalid day"),
        };
}
