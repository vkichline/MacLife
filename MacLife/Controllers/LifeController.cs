using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MacLife.Models;

namespace MacLife.Controllers
{
    [Route("api/[controller]")]
    public class LifeController : Controller
	{
		private World myworld;
        private Random r = new Random(Environment.TickCount);

        public LifeController(World world)
		{
			myworld = world;
		}

		// GET api/values
		[HttpGet]
		public World Get()
		{
            myworld.Regenerate();  // Do this asynchonously
            return myworld;
		}
	}
}
