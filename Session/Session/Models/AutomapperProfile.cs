using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Session.Model;
using Session.ViewModels;
using Session.Repository;
using System.Data;

namespace Session.Models
{
    /// <summary>
    /// auto mapper profile contains all required maping
    /// and this class initaited on Application_Start
    /// </summary>
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<SessionTopic, SessionTopicViewModel>();
            CreateMap<SessionTopicViewModel, SessionTopic>();

            CreateMap<SessionRoom, SessionRoomViewModel>();
            CreateMap<SessionRoomViewModel, SessionRoom>();

            CreateMap<SessionClass, SessionClassViewModel>()
                .ForMember(dist => dist.RoomName, opt => opt.MapFrom(s => s.SessionRoom.Name))
                .ForMember(dist => dist.TopicName, opt => opt.MapFrom(s => s.SessionTopic.Name));

            CreateMap<SessionClassViewModel, SessionClass>();


            CreateMap<DataTable, SessionAttendeeViewModel>()
                 .ForMember(dist => dist.DataHeader, opt => opt.MapFrom(s => ConvertToSessionAttendHeader(s.Columns)))
                 .ForMember(dist => dist.DataValue, opt => opt.MapFrom(s => ConvertToSessionAttendData(s.Rows)));


        }
        private List<string> ConvertToSessionAttendHeader(DataColumnCollection columns)
        {
            var Header = new List<string>();
           
            //take top 5 from full data
            for (int i= 0;i < columns.Count && i<5;i++)
                Header.Add(columns[i].ColumnName);
            
            return Header;

        }

        private List<List<string>> ConvertToSessionAttendData(DataRowCollection rows)
        {
            
            var data =new List<List<string>>();
            for (int i = 0; i < rows.Count; i++)
            {
                data.Add(new List<string>());
                for (int j=0;j< rows[i].ItemArray.Count() && j < 5; j++)
                data[i].Add(rows[i][j].ToString());
            }
                return data;
        }

     
    }

}