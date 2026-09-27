select *
from product
where active==true;
select *
from products
where stock<5;
select *
from customer
where order !=null;
select c.id,c.name,sum(o.total) as totall
FROM Customers c
INNER JOIN [order] o
    ON c.id = o.customer_id
GROUP BY c.name;