namespace Bookshop_Business.Utilities;

public static class DiscountCalculator
{
    public static double CalculateDiscount(double price, double discount)
    {
        if(discount == 0)
        {
            return price;
        }
        var result = (price * discount) / 100;
        return price - result;
    }
}
