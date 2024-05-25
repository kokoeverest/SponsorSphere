using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IPictureRepository
    {
        /// <summary>
        /// Asynchronously creates a new picture.
        /// </summary>
        /// <param name="picture">The picture entity to be created.</param>
        /// <returns>The created <see cref="Picture"/> entity.</returns>
        Task<Picture> CreateAsync(Picture picture);

        /// <summary>
        /// Asynchronously updates an existing picture.
        /// </summary>
        /// <param name="picture">The updated picture DTO containing the new values.</param>
        /// <returns>The updated <see cref="PictureDto"/>.</returns>
        Task<PictureDto> UpdateAsync(PictureDto picture);

        /// <summary>
        /// Asynchronously deletes a picture.
        /// </summary>
        /// <param name="picture">The picture DTO identifying the picture to be deleted.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> DeleteAsync(PictureDto picture);
    }
}
