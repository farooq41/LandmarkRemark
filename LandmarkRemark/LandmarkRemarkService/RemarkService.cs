using LandmarkRemarkData;
using LandmarkRemarkModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandmarkRemarkService
{
    public class RemarkService : IRemarkService
    {
        private Context _context;
        public RemarkService(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Remark>> GetAllRemarks()
        {
            return await _context.Remarks.Select(r => 
            new Remark { Id = r.Id, Note = r.Note, Latitude = r.Latitude, Longitude = r.Longitude, User = new User { Username = r.User.Username }, CreatedDate = r.CreatedDate, UserId = r.UserId })
            .ToListAsync();
        }

        public async Task<int> RemarkLandmark(Remark remark)
        {
            try
            {
                remark.UserId = remark.User.Id;
                remark.User = null;
                var result = _context.Remarks.Add(remark);
                await _context.SaveChangesAsync();
                return result.Entity.Id;
            }
            catch (Exception e)
            {

            }
            return 0;

        }
    }
}
