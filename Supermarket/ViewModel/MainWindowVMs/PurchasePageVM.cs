using Supermarket.Model;
using Supermarket.Model.BusinessLogic;
using Supermarket.Utilities;
using Supermarket.ViewModel.Commands;
using Supermarket.ViewModel.DataVM;
using System;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Windows;
using System.Windows.Input;

namespace Supermarket.ViewModel.MainWindowVMs
{
	internal class PurchasePageVM : BaseVM
	{
		#region Properties

		private ReceiptVM _receipt;
		public ReceiptVM Receipt
		{
			get { return _receipt; }
			set
			{
				_receipt = value;
				OnPropertyChanged(nameof(Receipt));
			}
		}

		private ReceiptItemVM _receiptItem;
		public ReceiptItemVM ReceiptItem
		{
			get { return _receiptItem; }
			set
			{
				_receiptItem = value;
				OnPropertyChanged(nameof(ReceiptItem));
			}
		}

		private bool _doFilter = true;

		private string _productName;
		public string ProductName
		{
			get { return _productName; }
			set
			{
				_productName = value;
				OnPropertyChanged(nameof(ProductName));
				if (_doFilter && !Functions.AreNotNullOrEmpty(value))
				{
					_doFilter = false;
					Barcode = string.Empty;
					_doFilter = true;
					return;
				}

				if (_doFilter)
				{
					_doFilter = false;

					var exactProduct = Cache.Instance.Products.Find((p) => p.Name == ProductName);
					if (exactProduct != null)
					{
						Barcode = exactProduct.Barcode;
					}

					else
					{
						var product = Cache.Instance.Products.Find((p) => p.Name.Contains(ProductName));
						if (product != null)
						{
							Barcode = product.Barcode;
						}
						else
						{
							Barcode = string.Empty;
						}
					}

					_doFilter = true;
				}
			}
		}

		private string _barcode;
		public string Barcode
		{
			get { return _barcode; }
			set
			{
				_barcode = value;
				OnPropertyChanged(nameof(Barcode));

				if (_doFilter && !Functions.AreNotNullOrEmpty(value))
				{
					_doFilter = false;
					ProductName = string.Empty;
					_doFilter = true;
					return;
				}

				if (_doFilter)
				{
					_doFilter = false;

					var exactProduct = Cache.Instance.Products.Find((p) => p.Barcode == Barcode);
					if (exactProduct != null)
					{
						ProductName = exactProduct.Name;
					}

					else
					{
						var product = Cache.Instance.Products.Find((p) => p.Barcode.Contains(Barcode));
						if (product != null)
						{
							ProductName = product.Name;
						}
						else
						{
							ProductName = string.Empty;
						}
					}

					_doFilter = true;
				}
			}
		}

		private int _quantity;
		public int Quantity
		{
			get { return _quantity; }
			set
			{
				_quantity = value;
				OnPropertyChanged(nameof(Quantity));
				Subtotal = Quantity.ToString();
			}
		}

		private string _subtotal;
		public string Subtotal
		{
			get { return _subtotal; }
			set
			{
				_subtotal = value;
				OnPropertyChanged(nameof(Subtotal));
			}
		}

		#endregion

		public ICommand AddItemCommand { get; }
		public ICommand SaveReceiptCommand { get; }

		public PurchasePageVM()
		{
			Receipt = new ReceiptVM();
			_doFilter = false;
			ResetReceiptItem();
			_doFilter = true;

			AddItemCommand = new RelayCommandVoid(AddItem);
			SaveReceiptCommand = new RelayCommandVoid(SaveReceipt);
		}

		private void AddItem()
		{
			if (!Functions.AreNotNullOrEmpty(ProductName, Barcode))
			{
				Functions.LogError("The product name cannot be empty");
				return;
			}

			var productModel = Cache.Instance.Products.Find((p) => p.Name == ProductName && p.Barcode == Barcode);
			if (productModel == null)
			{
				Functions.LogError("The product does not exist");
				return;
			}

			if (Quantity <= 0)
			{
				Functions.LogError("The quantity must be greater than 0");
				return;
			}

			var categoryVM = new CategoryVM(Cache.Instance.Categories.Find((c) => c.ID == productModel.CategoryID));
			var producerVM = new ProducerVM(Cache.Instance.Producers.Find((p) => p.ID == productModel.ProducerID));
			if (categoryVM == null || producerVM == null)
			{
				Functions.LogError("The category or producer does not exist");
				return;
			}

			ReceiptItem.Product = new ProductVM(productModel.ID, ProductName, Barcode, categoryVM, producerVM);
			Receipt.Items.Add(ReceiptItem);
			ResetReceiptItem();
		}

		private void SaveReceipt()
		{
			if (Receipt.Items.Count == 0)
			{
				Functions.LogError("The receipt must have at least one item");
				return;
			}

			if (ReceiptBL.CreateReceipt(Receipt))
			{
				Functions.LogInfo("The receipt was successfully created");
				Receipt = new ReceiptVM();
				ResetReceiptItem();
			}
		}

		private void ResetReceiptItem()
		{
			ReceiptItem = new ReceiptItemVM();
			ProductName = string.Empty;
			Barcode = string.Empty;
			Quantity = 0;
		}
	}
}
