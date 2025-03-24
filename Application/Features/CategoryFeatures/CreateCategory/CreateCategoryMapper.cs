﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entity.Product;

namespace Application.Features.CategoryFeatures.CreateCategory
{
    public sealed class CreateCategoryMapper : Profile
    {
        public CreateCategoryMapper()
        {
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<Category, CreateCategoryResponse>();
        }
    }
}
