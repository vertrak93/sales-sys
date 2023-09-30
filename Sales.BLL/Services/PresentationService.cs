using Microsoft.EntityFrameworkCore;
using Sales.Data.UnitOfWork;
using Sales.DTOs;
using Sales.Models;
using Sales.Utils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services
{
    public class PresentationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PresentationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddPresentation(Presentation presentation)
        {
            await _unitOfWork.Presentations.Add(presentation);
        }

        public async Task<IEnumerable<PresentationDto>> GetPresentations()
        {
            var obj = await _unitOfWork.Presentations.Get().ToListAsync();

            return obj.Where(o => o.Active == true).Select(o => {
                return new PresentationDto
                {
                    PresentationId = o.PresentationId,
                    PresentationName = o.PresentationName
                };
            });
        }

        public bool UpdatePresentation(Presentation presentation)
        {
            var obj = _unitOfWork.Presentations.Update(presentation);
            return obj;
        }

        public async Task<bool> DeletePresentation(Presentation presentation)
        {
            await ValidateDeletePresentation(presentation);
            var obj = await _unitOfWork.Presentations.Delete(presentation.PresentationId);
            return obj;
        }

        public async Task ValidateDeletePresentation(Presentation presentation)
        {
            var obj = await _unitOfWork.Products.Get().Where(o => o.PresentationId == presentation.PresentationId && o.Active == true).ToListAsync();

            if (obj.Any())
            {
                throw new Exception(Messages.PresentationUsed);
            }
        }
    }
}
