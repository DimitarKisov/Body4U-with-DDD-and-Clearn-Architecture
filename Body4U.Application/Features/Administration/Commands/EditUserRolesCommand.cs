﻿namespace Body4U.Application.Features.Administration.Commands
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class EditUserRolesCommand : IRequest<Result>
    {
        public EditUserRolesCommand(string email, IEnumerable<string> rolesIds)
        {
            this.Email = email;
            this.RolesIds = rolesIds;
        }

        public string Email { get; }

        public IEnumerable<string> RolesIds { get; }

        public class EditUserRolesCommandHandler : IRequestHandler<EditUserRolesCommand, Result>
        {
            private readonly IIdentityService identityService;

            public EditUserRolesCommandHandler(IIdentityService identityService)
                => this.identityService = identityService;

            public Task<Result> Handle(EditUserRolesCommand request, CancellationToken cancellationToken)
                => this.identityService.EditUserRoles(request, cancellationToken);
        }
    }
}