import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'category-list',
  templateUrl: '/category-list.component.html',
  styleUrls: ['/category-list.component.scss']
})
export class CategoryListComponent implements OnInit {
  
  categories = [{ name: "Medicamentos" },{ name: "Cosméticos" },{ name: "Perfumaria" },{ name: "Maquiagem" }]
  
  constructor() { }

  ngOnInit() {}
  
  openCategoryPage(category)
  {
    alert('Open Category ' + category.name + ' Page')  
  }
} 
