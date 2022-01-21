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
            double workDays = dayCount;

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
                    if (weekEnd.StartDate == weekEnd.EndDate & (startDate.AddDays(workDays + 1) < weekEnd.StartDate || startDate > weekEnd.EndDate))
                        workDays++;
                    if (!(startDate.AddDays(workDays - 1) < weekEnd.StartDate || startDate > weekEnd.EndDate))
                    {
                        if (weekEnd.StartDate == weekEnd.EndDate && weekEnd.EndDate > startDate)
                            workDays++;
                        else if (startDate > weekEnd.StartDate & startDate <= weekEnd.EndDate)
                            workDays = workDays + (weekEnd.EndDate - startDate).TotalDays;
                        else 
                        {
                            if (weekEnd.StartDate == weekEnd.EndDate & (weekEnd.EndDate - startDate).TotalDays == 0 & weekEnd.StartDate != startDate) 
                                workDays++;
                            else workDays += (weekEnd.EndDate - weekEnd.StartDate).TotalDays;
                        }
                    }
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
