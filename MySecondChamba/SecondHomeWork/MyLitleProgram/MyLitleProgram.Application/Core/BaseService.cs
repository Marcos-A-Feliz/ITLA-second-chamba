namespace MyLitleProgram.Application.Core
{
    public abstract class BaseService<TDto, TCreateDto, TUpdateDto>
        : IBaseService<TDto, TCreateDto, TUpdateDto>
    {
        public abstract Task<ServiceResult<IEnumerable<TDto>>> GetAllAsync();
        public abstract Task<ServiceResult<TDto>> GetByIdAsync(int id);
        public abstract Task<ServiceResult<TDto>> CreateAsync(TCreateDto createDto);
        public abstract Task<ServiceResult<TDto>> UpdateAsync(int id, TUpdateDto updateDto);
        public abstract Task<ServiceResult<bool>> DeleteAsync(int id);

        protected ServiceResult<T> Ok<T>(T data, string? message = null) =>
            ServiceResult<T>.Ok(data, message);

        protected ServiceResult<T> Fail<T>(string error) =>
            ServiceResult<T>.Fail(error);
    }
}
