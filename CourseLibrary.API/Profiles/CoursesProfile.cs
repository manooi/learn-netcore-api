﻿using AutoMapper;

namespace CourseLibrary.API.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            // source, destination
            CreateMap<Entities.Course, Models.CourseDto>();
            CreateMap<Models.CourseForCreationDto, Entities.Course>();
            CreateMap<Models.CourseForUpdateDto, Entities.Course>();
            CreateMap<Entities.Course, Models.CourseForUpdateDto>();
        }
    }
}