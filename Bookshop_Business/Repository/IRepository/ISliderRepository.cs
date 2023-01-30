
namespace Bookshop_Business.Repository.IRepository;

public interface ISliderRepository
{
    public Task<Tuple<List<SlidersDTO>, int>> GetAllSliders(DateTime startDate, DateTime endDate, int pageId = 1,  string SliderTitle = "");
    public Task<List<SlidersDTO>> GetActiveSliders();
    public Task<SlidersDTO> GetSliderById(int sliderId);
    public Task<bool> AddSlider(SlidersDTO sliderDto);
    public Task<bool> UpdateSlider(int sliderId, SlidersDTO sliderDto);
    public Task<bool> DeleteSliderById(int sliderId);
    //public Task<SlidersDTO> GetCurrentSlider();
}
