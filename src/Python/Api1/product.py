from datetime import datetime

class Product:
    product_id: int
    category_name: str
    product_name: str
    color: str
    standard_cost: float
    sell_start_date: datetime

    def __init__(self, product_id, category_name, product_name, color, standard_cost, sell_start_date):
        self.product_id = product_id
        self.category_name = category_name
        product_name = product_name
        color = color
        standard_cost = standard_cost
        sell_start_date = sell_start_date