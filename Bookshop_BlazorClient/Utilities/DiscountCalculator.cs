namespace Bookshop_BlazorClient.Utilities;

public static class DiscountCalculator
{
    public static double CalculateDiscount(double price, double discount)
    {
        var result = (price * discount) / 100;
        return price - result;
    }
}
