﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMeat.DAL;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SMeat.DAL.Abstract;
using SMeat.MODELS.Entities;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class BoardsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BoardsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBoards([FromQuery] int page, [FromQuery] int count, [FromQuery] string searchBy)
        {
            Expression<Func<Board, bool>> filter = null;
            if (searchBy != null)
            {
                filter = (b => b.Name.Contains(searchBy));
            }

            var boards = await _unitOfWork.BoardsRepository.GetPagedAsync(filter: filter, count: count, page: page);
            var boardsCount = await _unitOfWork.BoardsRepository.CountAsync(filter: filter);

            return Ok(boards);
        }
    }
}
