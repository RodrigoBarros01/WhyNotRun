﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhyNotRun.Models.UserViewModel
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Email de usuário é obrigatório."),
        RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória para um usuário.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Profissão é obrigatória.")]
        public string Profession { get; set; }

        public string Picture { get; set; }

        public User ToUser()
        {
            return new User
            {
                Id = ObjectId.GenerateNewId(),
                Name = Name,
                Password = Password,
                Picture = Picture,
                Email = Email
            };
        }
    }
}