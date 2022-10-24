﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class PredmetController : ControllerBase
{
        private readonly IPredmet _IPredmet;
        public PredmetController(IPredmet iPredmet)
        {
            _IPredmet = iPredmet;
        }
        //získání všech předmětu --> proběhne při prvotním vytvoření Teamu 
        [HttpGet]
        public async Task<List<Predmet>> Get()
        {
            return await Task.FromResult(_IPredmet.GetPredmety());
        }
        //získání počtů kapitol patřící pod jednotlivý předmět --> použití u souhrnu 
        [HttpGet("{IDPredmetu}")]
        public async Task<int> Get(int IDPredmetu)
        {
            return await Task.FromResult(_IPredmet.GetCountKapitoly(IDPredmetu));
        }
    }
}