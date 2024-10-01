using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers
{
    [ApiController]
    [Route("api/campaign")]
    public class CampaignController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CampaignController(ApplicationDBContext context)
        {
            _context=context;
            
        }
        
    }
}