﻿namespace YanZhiwei.DotNet2.Utilities.Common
{
    using Enum;
    using System;
    using System.Globalization;
    
    /// <summary>
    /// Date 帮助类
    /// </summary>
    public static class DateHelper
    {
        #region Fields
        
        /// <summary>
        /// 一天分钟数
        /// </summary>
        public const int MinutesOfTheDay = 1440;
        
        /// <summary>
        /// 秒
        /// </summary>
        public const int SECOND = 1,
                         MINUTE = 60 * SECOND,
                         HOUR = 60 * MINUTE,
                         DAY = 24 * HOUR,
                         MONTH = 30 * DAY;
                         
        #endregion Fields
        
        #region Methods
        
        /// <summary>
        /// 一天末尾时间
        /// </summary>
        /// <param name="data">需要操作的时间</param>
        /// <returns>DateTime</returns>
        public static DateTime EndOfDay(this DateTime data)
        {
            return new DateTime(data.Year, data.Month, data.Day).AddDays(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }
        
        /// <summary>
        /// 一个月末尾时间
        /// </summary>
        /// <param name="data">需要操作的时间</param>
        /// <returns>DateTime</returns>
        public static DateTime EndOfMonth(this DateTime data)
        {
            return new DateTime(data.Year, data.Month, 1).AddMonths(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }
        
        /// <summary>
        /// 一周末尾时间
        /// </summary>
        /// <param name="date">需要操作的时间</param>
        /// <returns>DateTime</returns>
        public static DateTime EndOfWeek(this DateTime date)
        {
            return EndOfWeek(date, DayOfWeek.Monday);
        }
        
        /// <summary>
        /// 一周末尾时间
        /// </summary>
        /// <param name="date">需要操作的时间</param>
        /// <param name="startDayOfWeek">一周起始周期</param>
        /// <returns>DateTime</returns>
        public static DateTime EndOfWeek(this DateTime date, DayOfWeek startDayOfWeek)
        {
            DateTime _endDate = date;
            DayOfWeek endDayOfWeek = startDayOfWeek - 1;
            
            if(endDayOfWeek < 0)
            {
                endDayOfWeek = DayOfWeek.Saturday;
            }
            
            if(_endDate.DayOfWeek != endDayOfWeek)
            {
                if(endDayOfWeek < _endDate.DayOfWeek)
                {
                    _endDate = _endDate.AddDays(7 - (_endDate.DayOfWeek - endDayOfWeek));
                }
                else
                {
                    _endDate = _endDate.AddDays(endDayOfWeek - _endDate.DayOfWeek);
                }
            }
            
            return new DateTime(_endDate.Year, _endDate.Month, _endDate.Day, 23, 59, 59, 999);
        }
        
        /// <summary>
        /// 一年末尾时间
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns>DateTime</returns>
        public static DateTime EndOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1).AddYears(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }
        
        /// <summary>
        /// 一个星期的第一天
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns>DateTime</returns>
        public static DateTime FirstDayOfWeek(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day).AddDays(-(int)date.DayOfWeek);
        }
        
        /// <summary>
        /// 格式化日期时间
        /// <para>0==>yyyy-MM-dd</para>
        /// <para>1==>yyyy-MM-dd HH:mm:ss</para>
        /// <para>2==>yyyy/MM/dd</para>
        /// <para>3==>yyyy年MM月dd日</para>
        /// <para>4==>MM-dd</para>
        /// <para>5==>MM/dd</para>
        /// <para>6==>MM月dd日</para>
        /// <para>8==>yyyy-MM</para>
        /// <para>9==>yyyy/MM</para>
        /// <para>9==>yyyy年MM月</para>
        /// <para>10==>HH:mm:ss</para>
        /// <para>11==>HH:mm</para>
        /// <para>12==>yyyyMMddHHmmss</para>
        /// <para>13==>yyyyMMdd</para>
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="dateMode">显示模式</param>
        /// <returns>0-9种模式的日期</returns>
        public static string FormatDate(this DateTime dateTime, int dateMode)
        {
            switch(dateMode)
            {
                case 0:
                    return dateTime.ToString("yyyy-MM-dd");
                    
                case 1:
                    return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    
                case 2:
                    return dateTime.ToString("yyyy/MM/dd");
                    
                case 3:
                    return dateTime.ToString("yyyy年MM月dd日");
                    
                case 4:
                    return dateTime.ToString("MM-dd");
                    
                case 5:
                    return dateTime.ToString("MM/dd");
                    
                case 6:
                    return dateTime.ToString("MM月dd日");
                    
                case 7:
                    return dateTime.ToString("yyyy-MM");
                    
                case 8:
                    return dateTime.ToString("yyyy/MM");
                    
                case 9:
                    return dateTime.ToString("yyyy年MM月");
                    
                case 10:
                    return dateTime.ToString("HH:mm:ss");
                    
                case 11:
                    return dateTime.ToString("HH:mm");
                    
                case 12:
                    return dateTime.ToString("yyyyMMddHHmmss");
                    
                case 13:
                    return dateTime.ToString("yyyyMMdd");
                    
                default:
                    return dateTime.ToString();
            }
        }
        
        /// <summary>
        /// 根据出生日期获取年龄
        /// </summary>
        /// <param name="birthDate">出生日期</param>
        /// <returns>年龄</returns>
        public static int GetAge(this DateTime birthDate)
        {
            if(DateTime.Today.Month < birthDate.Month || (DateTime.Today.Month == birthDate.Month && DateTime.Today.Day < birthDate.Day))
            {
                return DateTime.Today.Year - birthDate.Year - 1;
            }
            
            return DateTime.Today.Year - birthDate.Year;
        }
        
        /// <summary>
        /// 日期差计算
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="part">时间差枚举</param>
        /// <returns>时间差</returns>
        public static int GetDateDiff(this DateTime startTime, DateTime endTime, DateType part)
        {
            int _resutl = 0;
            
            switch(part)
            {
                case DateType.year:
                    _resutl = endTime.Year - startTime.Year;
                    break;
                    
                case DateType.month:
                    _resutl = (endTime.Year - startTime.Year) * 12 + (endTime.Month - startTime.Month);
                    break;
                    
                case DateType.day:
                    _resutl = (int)(endTime - startTime).TotalDays;
                    break;
                    
                case DateType.hour:
                    _resutl = (int)(endTime - startTime).TotalHours;
                    break;
                    
                case DateType.minute:
                    _resutl = (int)(endTime - startTime).TotalMinutes;
                    break;
                    
                case DateType.second:
                    _resutl = (int)(endTime - startTime).TotalSeconds;
                    break;
            }
            
            return _resutl;
        }
        
        /// <summary>
        /// 获取一个月有多少天
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns>一个月有多少天</returns>
        public static int GetDays(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }
        
        /// <summary>
        /// 友好时间
        /// </summary>
        /// <param name="datetime">DateTime</param>
        /// <returns>友好时间</returns>
        public static string GetFriendlyString(this DateTime datetime)
        {
            string _friendlyString = string.Empty;
            TimeSpan _ts = DateTime.Now - datetime;
            double _totalSeconds = _ts.TotalSeconds;
            
            if(_totalSeconds < 1 * SECOND)
            {
                _friendlyString = _ts.Seconds == 1 ? "1秒前" : _ts.Seconds + "秒前";
            }
            else if(_totalSeconds < 2 * MINUTE)
            {
                _friendlyString = "1分钟之前";
            }
            else if(_totalSeconds < 45 * MINUTE)
            {
                _friendlyString = _ts.Minutes + "分钟";
            }
            else if(_totalSeconds < 90 * MINUTE)
            {
                _friendlyString = "1小时前";
            }
            else if(_totalSeconds < 24 * HOUR)
            {
                _friendlyString = _ts.Hours + "小时前";
            }
            else if(_totalSeconds < 48 * HOUR)
            {
                _friendlyString = "昨天";
            }
            else if(_totalSeconds < 30 * DAY)
            {
                _friendlyString = _ts.Days + " 天之前";
            }
            else if(_totalSeconds < 12 * MONTH)
            {
                int _months = Convert.ToInt32(Math.Floor((double)_ts.Days / 30));
                _friendlyString = _months <= 1 ? "一个月之前" : _months + "月之前";
            }
            else
            {
                int _years = Convert.ToInt32(Math.Floor((double)_ts.Days / 365));
                _friendlyString = _years <= 1 ? "一年前" : _years + "年前";
            }
            
            return _friendlyString;
        }
        
        /// <summary>
        /// 计算两个时间之间工作天数
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>工作天数 </returns>
        public static int GetWeekdays(DateTime startTime, DateTime endTime)
        {
            TimeSpan _ts = endTime - startTime;
            int _weekCount = 0;
            
            for(int i = 0; i < _ts.Days; i++)
            {
                DateTime _time = startTime.AddDays(i);
                
                if(IsWeekDay(_time))
                {
                    _weekCount++;
                }
            }
            
            return _weekCount;
        }
        
        /// <summary>
        /// 计算两个时间直接周末天数
        /// </summary>
        /// <param name="startTime">开始天数</param>
        /// <param name="endTime">结束天数</param>
        /// <returns>周末天数</returns>
        public static int GetWeekends(DateTime startTime, DateTime endTime)
        {
            TimeSpan _ts = endTime - startTime;
            int _weekendCount = 0;
            
            for(int i = 0; i < _ts.Days; i++)
            {
                DateTime dt = startTime.AddDays(i);
                
                if(IsWeekEnd(dt))
                {
                    _weekendCount++;
                }
            }
            
            return _weekendCount;
        }
        
        /// <summary>
        /// 获取日期是一年中第几个星期
        /// </summary>
        /// <param name="date">需要计算的时间</param>
        /// <returns>一年中第几个星期</returns>
        public static int GetWeekNumber(this DateTime date)
        {
            var _cultureInfo = CultureInfo.CurrentCulture;
            return _cultureInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
        
        /// <summary>
        /// 是否是下午时间
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns>下午时间</returns>
        public static bool IsAfternoon(this DateTime date)
        {
            return date.TimeOfDay >= new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
        }
        
        /// <summary>
        /// 日期部分比较
        /// </summary>
        /// <param name="date">时间一</param>
        /// <param name="dateToCompare">时间二</param>
        /// <returns>日期部分是否相等</returns>
        public static bool IsDateEqual(this DateTime date, DateTime dateToCompare)
        {
            return date.Date == dateToCompare.Date;
        }
        
        /// <summary>
        /// 是否是将来时间
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns> 是否是将来时间</returns>
        public static bool IsFuture(this DateTime date)
        {
            return date > DateTime.Now;
        }
        
        /// <summary>
        /// 是否是上午时间
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns>是否是上午时间</returns>
        public static bool IsMorning(this DateTime date)
        {
            return date.TimeOfDay < new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
        }
        
        /// <summary>
        /// 是否是当前时间
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns>是否是当前时间</returns>
        public static bool IsNow(this DateTime date)
        {
            return date == DateTime.Now;
        }
        
        /// <summary>
        /// 是否是过去时间
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns>是否是过去时间</returns>
        public static bool IsPast(this DateTime date)
        {
            return date < DateTime.Now;
        }
        
        /// <summary>
        /// 时间部分比较
        /// </summary>
        /// <param name="time">时间一</param>
        /// <param name="timeToCompare">时间二</param>
        /// <returns>时间是否一致</returns>
        public static bool IsTimeEqual(this DateTime time, DateTime timeToCompare)
        {
            return time.TimeOfDay == timeToCompare.TimeOfDay;
        }
        
        /// <summary>
        /// 日期是否是今天
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>是否是今天</returns>
        public static bool IsToday(this DateTime date)
        {
            return date.Date == DateTime.Today;
        }
        
        /// <summary>
        /// 是否是工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>是否是工作日</returns>
        public static bool IsWeekDay(this DateTime date)
        {
            return !(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
        }
        
        /// <summary>
        ///  是否是周末
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>是否是周末</returns>
        public static bool IsWeekEnd(this DateTime dt)
        {
            return Convert.ToInt16(dt.DayOfWeek) > 5;
        }
        
        /// <summary>
        /// 是否周末
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>是否周末</returns>
        public static bool IsWeekendDay(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
        
        /// <summary>
        /// 一周最后一天
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>一周最后一天</returns>
        public static DateTime LastDayOfWeek(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day).AddDays(6 - (int)date.DayOfWeek);
        }
        
        /// <summary>
        /// 时间字符串转换为时间类型
        /// </summary>
        /// <param name="data">需要转换的时间字符串</param>
        /// <param name="format">格式</param>
        /// <returns>若转换时间失败，则返回默认事件值</returns>
        public static DateTime ParseDateTimeString(this string data, string format)
        {
            return DateTime.ParseExact(data, format, CultureInfo.InvariantCulture);
        }
        
        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <param name="second">秒</param>
        /// <returns>分钟</returns>
        public static int SecondToMinute(int second)
        {
            decimal _minute = (decimal)((decimal)second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(_minute));
        }
        
        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="current">需要设置的时间</param>
        /// <param name="hour">需要设置小时部分</param>
        /// <returns>设置后的时间</returns>
        public static DateTime SetTime(this DateTime current, int hour)
        {
            return SetTime(current, hour, 0, 0, 0);
        }
        
        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="current">需要设置的时间</param>
        /// <param name="hour">需要设置小时部分</param>
        /// <param name="minute">需要设置分钟部分</param>
        /// <returns>设置后的时间</returns>
        public static DateTime SetTime(this DateTime current, int hour, int minute)
        {
            return SetTime(current, hour, minute, 0, 0);
        }
        
        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="current">需要设置的时间</param>
        /// <param name="hour">小时</param>
        /// <param name="minute">分钟</param>
        /// <param name="second">秒</param>
        /// <returns>设置后的时间</returns>
        public static DateTime SetTime(this DateTime current, int hour, int minute, int second)
        {
            return SetTime(current, hour, minute, second, 0);
        }
        
        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="current">需要设置的时间.</param>
        /// <param name="hour">小时</param>
        /// <param name="minute">分钟</param>
        /// <param name="second">秒</param>
        /// <param name="millisecond">毫秒</param>
        /// <returns>设置后的时间</returns>
        public static DateTime SetTime(this DateTime current, int hour, int minute, int second, int millisecond)
        {
            return new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond);
        }
        
        /// <summary>
        ///  一天起始时间
        /// </summary>
        /// <param name="date">需要操作的时间</param>
        /// <returns>DateTime</returns>
        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }
        
        /// <summary>
        /// 一个月起始时间
        /// </summary>
        /// <param name="date">需要操作的时间</param>
        /// <returns>DateTime</returns>
        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        
        /// <summary>
        /// 一周起始时间
        /// </summary>
        /// <param name="date">需要操作的时间</param>
        /// <returns>DateTime</returns>
        public static DateTime StartOfWeek(this DateTime date)
        {
            return date.StartOfWeek(DayOfWeek.Monday);
        }
        
        /// <summary>
        /// 一周起始时间
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="startDayOfWeek">一周起始周天</param>
        /// <returns>一周起始时间</returns>
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startDayOfWeek)
        {
            DateTime _start = new DateTime(date.Year, date.Month, date.Day);
            
            if(_start.DayOfWeek != startDayOfWeek)
            {
                int d = startDayOfWeek - _start.DayOfWeek;
                
                if(startDayOfWeek <= _start.DayOfWeek)
                {
                    return _start.AddDays(d);
                }
                
                return _start.AddDays(-7 + d);
            }
            
            return _start;
        }
        
        /// <summary>
        /// 一年起始时间
        /// </summary>
        /// <param name="date">需要操作的时间</param>
        /// <returns>DateTime</returns>
        public static DateTime StartOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }
        
        /// <summary>
        /// 转换成EpochTime
        /// </summary>
        /// <param name="date">需要操作的时间</param>
        /// <returns>DateTime</returns>
        public static TimeSpan ToEpochTimeSpan(this DateTime date)
        {
            return date.Subtract(new DateTime(1970, 1, 1));
        }
        
        /// <summary>
        /// 明天时间
        /// </summary>
        /// <param name="date">需要操作的时间</param>
        /// <returns>DateTime</returns>
        public static DateTime Tomorrow(this DateTime date)
        {
            return date.AddDays(1);
        }
        
        /// <summary>
        /// 昨天时间
        /// </summary>
        /// <param name="date">需要操作的时间</param>
        /// <returns>DateTime</returns>
        public static DateTime Yesterday(this DateTime date)
        {
            return date.AddDays(-1);
        }
        
        /// <summary>
        /// 获取时间戳【毫秒】
        /// </summary>
        /// <returns>时间戳</returns>
        public static long GetTimeStamp()
        {
            return Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds);
        }
        
        #endregion Methods
    }
}