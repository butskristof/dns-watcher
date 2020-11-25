using System;
using DnsWatcher.Common.Exceptions;

namespace DnsWatcher.Domain.Common
{
	public abstract class EntityBase : IAuditableEntity, ISoftDeletableEntity
	{
		#region private properties

		private DateTime? _modifiedOn;

		#endregion

		public Guid Id { get; set; }

		#region auditing

		#region created

		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }

		#endregion

		#region modified

		public string ModifiedBy { get; set; }

		public DateTime? ModifiedOn
		{
			get => _modifiedOn;
			set
			{
				if (_modifiedOn.HasValue && value != _modifiedOn)
					throw new DataChangedException();

				_modifiedOn = value;
			}
		}

		public void SetModifiedOnForContext(DateTime value)
		{
			_modifiedOn = value;
		}

		#endregion

		#region deleted

		public string DeletedBy { get; set; }
		public DateTime? DeletedOn { get; set; }
		public bool Deleted => DeletedOn.HasValue;

		#endregion

		#endregion
	}
}