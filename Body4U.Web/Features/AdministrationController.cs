﻿namespace Body4U.Web.Features
{
    using Body4U.Application.Features.Administration.Queries.SearchUsers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.System;

    [Authorize(Roles = AdministratorRoleName)]
    public class AdministrationController : ApiController
    {
        [HttpPost]
        [Route(nameof(Search))]
        public async Task<ActionResult<SearchUsersOutputModel>> Search([FromQuery] SearchUsersQuery query)
            => await this.Send(query);
    }
}
