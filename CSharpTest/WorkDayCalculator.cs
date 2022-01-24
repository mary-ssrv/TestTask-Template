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
        /// <summary>
        /// is there a weekend before the start date
        /// </summary>
        /// <returns> start value for currentWeekEnd </returns>
        private int getCurrentWeekIndex(DateTime startDate, WeekEnd[] weekEnds)
        {
            int currentWeekEndIndex = 0;
            for (int i = 0; i < weekEnds.Length - 1; i++)
            {
                if (startDate >= weekEnds[i].StartDate & startDate <= weekEnds[i].EndDate)
                {
                    currentWeekEndIndex = i;
                    break;
                }
            }
            return currentWeekEndIndex;
        }
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            double workDays = dayCount;
            DateTime resultDate = startDate;

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
                    return resultDate.AddDays(dayCount - 1);

                int currentWeekEnd = getCurrentWeekIndex(resultDate, weekEnds);

                while (workDays >= 0)
                {
                    if (currentWeekEnd < weekEnds.Length)
                    {
                        WeekEnd weekEnd = weekEnds[currentWeekEnd];

                        if (resultDate >= weekEnd.StartDate & resultDate <= weekEnd.EndDate)
                        {
                            if ((weekEnd.EndDate - resultDate).TotalDays == 0)
                            {
                                workDays++;
                            }
                            else
                            {
                                workDays += (weekEnd.EndDate - resultDate).TotalDays + 1;
                            }

                            currentWeekEnd++;
                        }
                        else if (resultDate > weekEnd.EndDate)
                        {
                            currentWeekEnd++;
                        }
                    }
                    
                    workDays--;
                    if(workDays > 0)
                    {
                        resultDate = resultDate.AddDays(1);
                    }      
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return DateTime.Now;
            }

            return resultDate;
        }
    }
}
