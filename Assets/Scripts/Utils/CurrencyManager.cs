using System;
using System.Collections.Generic;

namespace GameJamEntry.CurrencyControl {
	public class CurrencyManager<T> where T : Enum {
		Dictionary<T, int> _currencies = new();
		
		public event Action<(T CurrencyType, int NewValue)> OnCurrencyValueChanged;
		
		public void AddCurrency(T type, int amount) {
			var currentValue = _currencies.GetValueOrDefault(type);
			_currencies[type] = currentValue + amount;
			OnCurrencyValueChanged?.Invoke((type, _currencies[type]));
		}
		
		public bool TrySpendCurrency(T type, int amount) {
			if ( !IsEnoughCurrency(type, amount) ) {
				return false;
			}
			var currentValue = _currencies.GetValueOrDefault(type);
			_currencies[type] = currentValue - amount;
			OnCurrencyValueChanged?.Invoke((type, _currencies[type]));
			return true;
		}
		
		public bool IsEnoughCurrency(T type, int amount) => _currencies.GetValueOrDefault(type) >= amount;
	}
}