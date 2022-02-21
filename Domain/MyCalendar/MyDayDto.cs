using System;

namespace Domain.MyCalendar
{
    public class MyDayDto
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        private int _month { get; set; }
        public int Month
        {
            get => _month;
            set
            {
               _month = value;

                switch (value)
                {
                    case 1:
                        MonthName = "Январь";
                        break;

                    case 2:
                        MonthName = "Февраль";
                        break;

                    case 3:
                        MonthName = "Март";
                        break;

                    case 4:
                        MonthName = "Апрель";
                        break;

                    case 5:
                        MonthName = "Май";
                        break;

                    case 6:
                        MonthName = "Июнь";
                        break;

                    case 7:
                        MonthName = "Июль";
                        break;

                    case 8:
                        MonthName = "Август";
                        break;

                    case 9:
                        MonthName = "Сентябрь";
                        break;

                    case 10:
                        MonthName = "Октябрь";
                        break;

                    case 11:
                        MonthName = "Ноябрь";
                        break;

                    case 12:
                        MonthName = "Декабрь";
                        break;

                    default:
                        MonthName = "Месяц";
                        break;
                }
            }
        }
        public int Day { get; set; }
        public int Week { get; set; }
        public int DayOfWeek { get; set; }
        public bool Today { get; set; } = false;
        public int BaseNumber { get; set; }
        public bool Filled { get; set; } = false;
        public int FilledNumber { get; set; }
        public int FilledNumberReverse { get; set; }
        public bool PreFilled { get; set; } = false;
        public int PreFilledNumber { get; set; } 
        public int PreFilledNumberReverse { get; set; } 
        public string MonthName { get; set; }

        public int Volume { get; set; }

    }
}