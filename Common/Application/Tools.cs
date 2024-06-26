﻿using System.Globalization;

namespace Common.Application;

public static class Tools
{
    private static readonly string[] _Pn = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
    private static readonly string[] _En = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    public static readonly string[] MonthNames = ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"];

    public static readonly string[] DayNames = ["شنبه", "یکشنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه"];
    public static readonly string[] DayNamesGregorian = ["یکشنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه", "شنبه"];

    public static string ToFarsi(this DateTime? date)
    {
        try
        {
            if (date != null) return date.Value.ToFarsi();
        }
        catch (Exception)
        {
            return "";
        }

        return "";
    }

    public static string ToFarsi(this DateTime date)
    {
        if (date == new DateTime()) return "";

        var pc = new PersianCalendar();
        return $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";
    }

    public static string GetTimePersian(this string date)
    {
        var dateTime = DateTime.Parse(date);
        var hour = dateTime.Hour == 0 ? 12 : dateTime.Hour;
        var minute = dateTime.Minute;
        var hourString = $"{hour:D2}";
        var minuteString = $"{minute:D2}";
        bool isAM = hour is >= 0 and < 12;
        var dayLight = isAM ? "صبح" : "عصر";
        return $"{dayLight} {hourString.ToPersianNumber()}:{minuteString.ToPersianNumber()}";
    }

    public static string ToDiscountFormat(this DateTime date) => date == new DateTime() ? "" : $"{date.Year}/{date.Month}/{date.Day}";

    public static string GetTime(this DateTime date) => $"_{date.Hour:00}_{date.Minute:00}_{date.Second:00}";

    public static string GetMonthShamsi(this string date)
    {
        var dateTime = DateTime.Parse(date);
        return MonthNames[dateTime.Month - 1];
    }

    public static string GetYearShamsi(this string date)
    {
        var dateTime = DateTime.Parse(date);
        return dateTime.Year.ToPersianNumber();
    }

    public static string GetDayWithPersianNumber(this string date)
    {
        var dateTime = DateTime.Parse(date);
        return dateTime.Day.ToPersianNumber().ToString();
    }

    public static string ToFarsiFull(this DateTime date)
    {
        var pc = new PersianCalendar();
        return $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00} {date.Hour:00}:{date.Minute:00}:{date.Second:00}";
    }

    public static string ToEnglishNumber(this string strNum)
    {
        var cash = strNum;
        for (var i = 0; i < 10; i++)
            cash = cash.Replace(_Pn[i], _En[i]);

        return cash;
    }

    public static string ToPersianNumber(this int intNum)
    {
        var chash = intNum.ToString();
        for (var i = 0; i < 10; i++)
            chash = chash.Replace(_En[i], _Pn[i]);

        return chash;
    }

    public static string ToPersianNumber(this string strNum)
    {
        var chash = strNum;
        for (var i = 0; i < 10; i++)
            chash = chash.Replace(_En[i], _Pn[i]);

        return chash;
    }

    public static DateTime? FromFarsiDate(this string InDate)
    {
        if (string.IsNullOrEmpty(InDate)) return null;

        var spited = InDate.Split('/');
        if (spited.Length < 3) return null;

        if (!int.TryParse(spited[0].ToEnglishNumber(), out var year)) return null;

        if (!int.TryParse(spited[1].ToEnglishNumber(), out var month)) return null;

        if (!int.TryParse(spited[2].ToEnglishNumber(), out var day)) return null;
        var c = new PersianCalendar();
        return c.ToDateTime(year, month, day, 0, 0, 0, 0);
    }

    public static DateTime ToGeorgianDateTime(this string persianDate)
    {
        persianDate = persianDate.ToEnglishNumber();
        var year = Convert.ToInt32(persianDate[..4]);
        var month = Convert.ToInt32(persianDate.Substring(5, 2));
        var day = Convert.ToInt32(persianDate.Substring(8, 2));
        return new DateTime(year, month, day, new PersianCalendar());
    }

    public static string ToMoney(this double myMoney) => myMoney.ToString("N0", CultureInfo.CreateSpecificCulture("fa-ir"));

    public static string ToFileName(this DateTime date) => $"{date.Year:0000}-{date.Month:00}-{date.Day:00}-{date.Hour:00}-{date.Minute:00}-{date.Second:00}";
}