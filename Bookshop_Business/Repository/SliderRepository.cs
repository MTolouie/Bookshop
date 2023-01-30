
namespace Bookshop_Business.Repository;

public class SliderRepository : ISliderRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public SliderRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<bool> AddSlider(SlidersDTO sliderDto)
    {
        try
        {
            var slider = _mapper.Map<SlidersDTO,Slider>(sliderDto);
            _db.Sliders.Add(slider);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteSliderById(int sliderId)
    {
        var slider = await _db.Sliders.FindAsync(sliderId);
        if (slider is null)
            return false;

        _db.Sliders.Remove(slider);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<List<SlidersDTO>> GetActiveSliders()
    {
        try
        {
            var sliders = await _db.Sliders
                .Where(s => s.StartDate.Date <= DateTime.Now.Date && s.EndDate.Date >= DateTime.Now.Date)
                .ToListAsync();

            var sliderDTO = _mapper.Map<List<Slider>,List<SlidersDTO>>(sliders);
            return sliderDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<Tuple<List<SlidersDTO>, int>> GetAllSliders(DateTime startDate, DateTime endDate, int pageId = 1, string SliderTitle = "")
    {
        try
        {
            IQueryable<Slider> sliders = _db.Sliders;

            if (!string.IsNullOrEmpty(SliderTitle))
            {
                sliders = sliders.Where(b => b.SliderTitle.Contains(SliderTitle));
            }

            if (DateTime.MinValue != startDate)
            {
                //DateTime FromDate = DateTime.ParseExact(fromdate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                sliders = sliders.Where(u => u.StartDate.Date.Equals(startDate.Date));
            }

            if (DateTime.MinValue != endDate)
            {
                //DateTime ToDate = DateTime.ParseExact(toDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                sliders = sliders.Where(u => u.EndDate.Date.Equals(endDate.Date));
            }


            var slidersDTOs = _mapper.Map<IQueryable<Slider>, List<SlidersDTO>>(sliders);

            int take = 5;
            int skip = (pageId - 1) * take;
            var pageCount = slidersDTOs.Count() / take;
            if (pageCount % 2 != 0)
            {
                pageCount++;
            }

            var query = slidersDTOs.OrderByDescending(c => c.StartDate).Skip(skip).Take(take).ToList();

            return Tuple.Create(query, pageCount);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    //public async Task<SlidersDTO> GetCurrentSlider()
    //{
    //    try
    //    {
    //        var slider = await _db.Sliders.OrderByDescending(s=>s.SliderId).Take(1).SingleOrDefaultAsync();
    //        if (slider is null)
    //            return null;

    //        var sliderDTO = _mapper.Map<Slider, SlidersDTO>(slider);
    //        return sliderDTO;
    //    }
    //    catch (Exception ex)
    //    {
    //        return null;
    //    }
    //}

    public async Task<SlidersDTO> GetSliderById(int sliderId)
    {
        try
        {
            var slider = await _db.Sliders.FindAsync(sliderId);
            if (slider is null)
                return null;

            var sliderDTO = _mapper.Map<Slider, SlidersDTO>(slider);
            return sliderDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> UpdateSlider(int sliderId, SlidersDTO sliderDto)
    {
        try
        {
            if (sliderId != sliderDto.SliderId)
                return false;


            var slider = await _db.Sliders.FindAsync(sliderId);
            if (slider is null)
                return false;

            var Updatedslider = _mapper.Map<SlidersDTO,Slider>(sliderDto, slider);

            _db.Sliders.Update(Updatedslider);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
