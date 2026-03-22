using MyLitleProgram.Application.Contract;
using MyLitleProgram.Application.Core;
using MyLitleProgram.Application.Dtos.Author;
using MyLitleProgram.Domain.Entities;
using MyLitleProgram.Infrastructure.Interfaces;

namespace MyLitleProgram.Application.Service
{
    public class AuthorService : BaseService<AuthorDto, CreateAuthorDto, UpdateAuthorDto>, IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public override async Task<ServiceResult<IEnumerable<AuthorDto>>> GetAllAsync()
        {
            try
            {
                var authors = await _authorRepository.GetAllAsync();

                var dtos = authors.Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Email = a.Email
                });

                return Ok<IEnumerable<AuthorDto>>(dtos);
            }
            catch (Exception ex)
            {
                return Fail<IEnumerable<AuthorDto>>($"Error al obtener los autores: {ex.Message}");
            }
        }

        public override async Task<ServiceResult<AuthorDto>> GetByIdAsync(int id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);

                if (author == null)
                    return Fail<AuthorDto>($"No se encontró el autor con Id {id}.");

                var dto = new AuthorDto
                {
                    Id = author.Id,
                    Name = author.Name,
                    Email = author.Email
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Fail<AuthorDto>($"Error al obtener el autor: {ex.Message}");
            }
        }

        public override async Task<ServiceResult<AuthorDto>> CreateAsync(CreateAuthorDto createDto)
        {
            try
            {
                // Validación: Name obligatorio
                if (string.IsNullOrWhiteSpace(createDto.Name))
                    return Fail<AuthorDto>("El nombre es obligatorio.");

                if (createDto.Name.Length > 100)
                    return Fail<AuthorDto>("El nombre no puede superar los 100 caracteres.");

                // Validación: Email obligatorio y formato válido
                if (string.IsNullOrWhiteSpace(createDto.Email))
                    return Fail<AuthorDto>("El email es obligatorio.");

                if (!IsValidEmail(createDto.Email))
                    return Fail<AuthorDto>("El formato del email no es válido.");

                if (createDto.Email.Length > 150)
                    return Fail<AuthorDto>("El email no puede superar los 150 caracteres.");

                var author = new Author
                {
                    Name = createDto.Name.Trim(),
                    Email = createDto.Email.Trim().ToLower()
                };

                var created = await _authorRepository.AddAsync(author);

                var dto = new AuthorDto
                {
                    Id = created.Id,
                    Name = created.Name,
                    Email = created.Email
                };

                return Ok(dto, "Autor creado exitosamente.");
            }
            catch (Exception ex)
            {
                return Fail<AuthorDto>($"Error al crear el autor: {ex.Message}");
            }
        }

        public override async Task<ServiceResult<AuthorDto>> UpdateAsync(int id, UpdateAuthorDto updateDto)
        {
            try
            {
                if (!await _authorRepository.ExistsAsync(id))
                    return Fail<AuthorDto>($"No se encontró el autor con Id {id}.");

                // Validación: Name obligatorio
                if (string.IsNullOrWhiteSpace(updateDto.Name))
                    return Fail<AuthorDto>("El nombre es obligatorio.");

                if (updateDto.Name.Length > 100)
                    return Fail<AuthorDto>("El nombre no puede superar los 100 caracteres.");

                // Validación: Email obligatorio y formato válido
                if (string.IsNullOrWhiteSpace(updateDto.Email))
                    return Fail<AuthorDto>("El email es obligatorio.");

                if (!IsValidEmail(updateDto.Email))
                    return Fail<AuthorDto>("El formato del email no es válido.");

                if (updateDto.Email.Length > 150)
                    return Fail<AuthorDto>("El email no puede superar los 150 caracteres.");

                var author = new Author
                {
                    Name = updateDto.Name.Trim(),
                    Email = updateDto.Email.Trim().ToLower()
                };

                await _authorRepository.UpdateAsync(id, author);

                var updated = await _authorRepository.GetByIdAsync(id);

                var dto = new AuthorDto
                {
                    Id = updated!.Id,
                    Name = updated.Name,
                    Email = updated.Email
                };

                return Ok(dto, "Autor actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return Fail<AuthorDto>($"Error al actualizar el autor: {ex.Message}");
            }
        }

        public override async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                if (!await _authorRepository.ExistsAsync(id))
                    return Fail<bool>($"No se encontró el autor con Id {id}.");

                await _authorRepository.DeleteAsync(id);
                return Ok(true, "Autor eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return Fail<bool>($"Error al eliminar el autor: {ex.Message}");
            }
        }

        // Método auxiliar para validar email
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email.Trim();
            }
            catch
            {
                return false;
            }
        }
    }
}
