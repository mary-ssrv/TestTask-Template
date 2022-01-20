using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    /// <summary>
    /// Calculator worker
    /// </summary>
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            int workDays = dayCount;

            try
            {
                if (workDays < 0)
                {
                    throw new Exception("dayCount can not be less than 0");
                }

                if (weekEnds != null)
                {
                    foreach (var weekEnd in weekEnds)
                    {
                        if (weekEnd.StartDate > weekEnd.EndDate)
                        {
                            throw new Exception("Incorrect data entered");
                        }
                    }
                }

                if (weekEnds == null)
                    return startDate.AddDays(dayCount - 1);

                foreach (var weekEnd in weekEnds)
                {
                    if (weekEnd.StartDate == weekEnd.EndDate && (startDate.AddDays(workDays + 1) < weekEnd.StartDate || startDate > weekEnd.EndDate))
                        workDays++;
                    if (!(startDate.AddDays(workDays - 1) < weekEnd.StartDate || startDate > weekEnd.EndDate))
                        workDays += weekEnd.EndDate.Day - weekEnd.StartDate.Day;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return DateTime.Now;
            }

            return startDate.AddDays(workDays);

        }
    }
}
