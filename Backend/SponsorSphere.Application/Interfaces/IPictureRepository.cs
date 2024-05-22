using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IPictureRepository
    {
        Task<Picture> CreateAsync(Picture picture);
        Task<PictureDto> UpdateAsync(PictureDto picture);
        Task<int> DeleteAsync(PictureDto picture);
    }
}
