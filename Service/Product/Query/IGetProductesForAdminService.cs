using Mohajer.Service.Admin.Product.Dto;
using Mohajer.Useful.Result;

namespace Mohajer.Service.Admin.Product.Query
{
    public interface IGetProductesForAdminService
    {
        ResultDto<ProductForAdminDto> Exequte(int PageSize, int PageNo ,bool confimeted);
    }
}
