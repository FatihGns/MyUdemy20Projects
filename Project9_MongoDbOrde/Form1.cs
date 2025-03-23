﻿using Project9_MongoDbOrde.Entities;
using Project9_MongoDbOrde.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Project9_MongoDbOrde
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OrderOperation orderOperation=new OrderOperation();
        private void btnCreate_Click(object sender, EventArgs e)
        {
            var order = new Order
            {
                City = txtCity.Text,
                CustomerName = txtCustomer.Text,
                District = txtDisrict.Text,
                TotalPrice = decimal.Parse(txtTotalPrice.Text),
            };
            orderOperation.AddOrder(order);
            MessageBox.Show("Ekleme İşlemi Yapıldı");
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            List<Order> orders = orderOperation.GetAllOrders();
            dataGridView1.DataSource=orders;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string orderId = txtId.Text;
            orderOperation.DeleteOrder(orderId);
            MessageBox.Show("Silme İşlemi Tamamlandı");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string orderId = txtId.Text;
            var updateOrder = new Order
            {
                City = txtCity.Text,
                CustomerName = txtCustomer.Text,
                District = txtDisrict.Text,
                TotalPrice = decimal.Parse(txtTotalPrice.Text),
                OrderId = orderId
            };
            orderOperation.UpdateOrder(updateOrder);
            MessageBox.Show("Güncelleme İşlemi Tamamlandı");

        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            Order orders = orderOperation.GetOrderById(id);
            dataGridView1.DataSource = new List<Order> {orders};
        }
    }
}
