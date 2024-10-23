using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FriendDTO
{
    public class FriendResDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FriendId { get; set; }
    }
}
