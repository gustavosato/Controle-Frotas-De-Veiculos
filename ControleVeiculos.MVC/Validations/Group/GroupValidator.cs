﻿using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Groups;

namespace Lean.Test.Cloud.MVC.Validations.Groups
{
    public class GroupValidator : AbstractValidator<GroupModel>
    {
        public GroupValidator()
        {
            //Campos obrigatórios
            RuleFor(x => x.GroupName).NotEmpty().WithMessage("O campo é obrigatório");

            //Limitação de caracteres
            RuleFor(x => x.GroupName).MaximumLength(150).WithMessage("O campo excedeu o limite de  150 caracteres");
        }
    }
}  