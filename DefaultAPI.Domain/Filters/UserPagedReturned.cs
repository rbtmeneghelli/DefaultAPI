using DefaultAPI.Domain.Base;
using DefaultAPI.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Filters
{
	public class UserPaged : BasePaged
	{
		#region Properties

		public List<UserReturnedDto> UserReturnedSet { get; set; }

		#endregion
	}
}
