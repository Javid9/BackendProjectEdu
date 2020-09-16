using AutoMapper;
using EducationBackendFinal.Models;
using EducationBackendFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Mapper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<UpComingEvent, UpComingGetVm>().ForMember(vm => vm.SpeakersId, o => o.MapFrom(

                    x => x.SpeakerEvents.Select(t => new SpeakerId{ Id = t.Speaker.Id }).ToList()));

        }
    }
}
