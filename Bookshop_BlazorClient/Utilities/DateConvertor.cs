using System.Globalization;

namespace Bookshop_BlazorClient.Utilities;

public static class DateConvertor
{
    public static string ToGregorian(this DateTime value)
    {
        var gc = new GregorianCalendar();
        return gc.GetYear(value) + "/" + gc.GetMonth(value).ToString("00") + "/" + gc.GetDayOfMonth(value).ToString("00");

    }


    public static string ToShamsi(this DateTime value)
    {
        PersianCalendar pc = new PersianCalendar();
        return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" + pc.GetDayOfMonth(value).ToString("00");
    }
}

