namespace MyLitleProgram.Application.Core
{
    public interface IBaseService<TDto, TCreateDto, TUpdateDto>
    {
        Task<ServiceResult<IEnumerable<TDto>>> GetAllAsync();
        Task<ServiceResult<TDto>> GetByIdAsync(int id);
        Task<ServiceResult<TDto>> CreateAsync(TCreateDto createDto);
        Task<ServiceResult<TDto>> UpdateAsync(int id, TUpdateDto updateDto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}
