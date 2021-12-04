using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maintance.Models;
using Maintance.Services;

using MvvmGen;
using MvvmGen.ViewModels;

using WPFCoreEx.Abstractions.Services;

namespace Maintance.ViewModels
{
	[ViewModel]
	[Inject(typeof(IMessageService))]
	[Inject(typeof(ViewsLocator))]
	public partial class MainViewModel
	{

		public ViewInfo SelectedView
		{
			set
			{
				if (value != null)
				{
#if DEBUG
					if (value.ViewModelType == null)
					{
						CurrentViewVM = null!;
						return;
					}
#endif
					CurrentViewVM = this.ViewsLocator.GetVM(value.ViewModelType);
				}
			}
		}

		private ViewModelBase _currentViewVM;
		public ViewModelBase CurrentViewVM
		{
			get => _currentViewVM;
			private set
			{
				if (value != _currentViewVM)
				{
					_currentViewVM = value;
					OnPropertyChanged(nameof(CurrentViewVM));
				}
			}
		}

		[Command]
		private void GetMainInfo()
		{
			MessageService.SendMessage(@"Лиза, я лишь хочу признаваться тебе,
Что я с детства влюблен, дать тепло батарей.
Холод осени с Лизой заменим весной;
Майский парк на скамейках - сидим мы с тобой.
Она мне говорит, как отлично, что я
Ей сказал, что взаимности толком не ждал;
Просто у меня есть уже план в голове,
Ведь с четвертого класса я думал о ней!
Лиза, ты будешь моей!
Лиза - май среди январей!
Лиза, я лишь хочу признаваться тебе,
Что я с детства влюблен, дать тепло батарей.
Холод осени с Лизой заменим весной;
Майский парк на скамейках - сидим мы с тобой;
Сидим мы с тобой.");
		}
	}
}
