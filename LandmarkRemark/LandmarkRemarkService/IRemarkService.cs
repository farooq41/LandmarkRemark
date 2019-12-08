using LandmarkRemarkModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LandmarkRemarkService
{
    public interface IRemarkService
    {
        Task<int> RemarkLandmark(Remark remark);
        Task<IEnumerable<Remark>> GetAllRemarks();
    }
}
