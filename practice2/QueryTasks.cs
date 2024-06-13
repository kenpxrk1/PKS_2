using practice2.Models;

namespace practice2;

public class QueryTasks
{
    // Вывести объекты недвижимости, расположенные в указанном районе стоимостью «ОТ» и «ДО»
    public void select_objects_by_district_and_price(int price_from, int price_to, string district)
    {

        using (Pks2Context db = new Pks2Context())
        {
            var properties = from obj in db.EstateObjects
                from districts in db.Districts
                where obj.Price >= price_from && obj.Price <= price_to && district == districts.DistrictName &&
                      obj.District == districts.DistrictId
                orderby obj.Price descending
                select new
                {
                    adress = obj.Adress,
                    square = obj.EstateObjectsSquare,
                    floor = obj.Floorr
                };
            int i = 1;
            foreach (var property in properties)
            {
                Console.WriteLine($"{i}. Адрес: {property.adress}, площадь: {property.square}, этаж: {property.floor}");
                i++;
            }
        }
    }

    public void select_realtors_solded_objects_by_rooms(int quantity_of_rooms)
    {
        using (Pks2Context db = new Pks2Context())
        {
            var rieltors = from rieltor in db.Realtors
                from sell in db.Sellings
                from property in db.EstateObjects
                where property.QuantityOfRooms == quantity_of_rooms && property.ObjectId == sell.ObjectId &&
                      rieltor.RealtorId == sell.RealtorId
                select new
                {
                    full_name = rieltor.RealtorName + " " + rieltor.RealtorLastname + " " + rieltor.RealtorSurname,
                };
            int i = 1;
            foreach (var rieltor in rieltors)
            {
                Console.WriteLine($"{i}. ФИО: {rieltor.full_name}");
                i++;
            }
        }

    }

    public void select_total_price_of_quantity_appartments_by_district(int quantity_of_rooms, string district)
    {
        using (Pks2Context db = new Pks2Context())
        {
            var array = from e in db.EstateObjects
                from district1 in db.Districts
                where e.District == district1.DistrictId && district1.DistrictName == district &&
                      e.QuantityOfRooms == quantity_of_rooms
                select new
                {
                    price = e.Price
                };
            float? total_sum = array.Sum(p => p.price);
            Console.WriteLine($"Общая цена недвижимости: {total_sum}");
        }
    }

    public void print_rieltor_by_min_and_max(string rieltor_name)
    {
        using (Pks2Context db = new Pks2Context())
        {
            var rieltor_sells = from e in db.EstateObjects
                from rieltor in db.Realtors
                from sell in db.Sellings
                where rieltor.RealtorName == rieltor_name && rieltor.RealtorId == sell.RealtorId &&
                      sell.ObjectId == e.ObjectId
                select new
                {
                    price = e.Price

                };
            var min = rieltor_sells.Min(p => p.price);
            var max = rieltor_sells.Max(p => p.price);
            Console.WriteLine($"Минимальная цена: {min}, максимальная цена: {max}");
        }

    }

    public void select_mark_by_rieltor_and_criteria(int property_type, string criteria_name, string rieltor_name)
    {
        using (Pks2Context db = new Pks2Context())
        {
            var marks = from criteria in db.GradeParameters
                from property in db.EstateObjects
                from rieltor in db.Realtors
                from mark in db.Grades
                where rieltor.RealtorName == rieltor_name && property.Types == property_type &&
                      criteria_name == criteria.ParamName && property.ObjectId == mark.ObjectId &&
                      mark.ParamId == criteria.ParamId
                select new
                {
                    markk = mark.Grade1
                };
            double? average_mark = marks.Average(p => p.markk);
            Console.WriteLine($"Средняя оценка: {average_mark}");
        }
    }

    public void select_by_floor(int floor)
    {
        using (Pks2Context db = new Pks2Context())
        {
            var props = from e in db.EstateObjects
                from district in db.Districts
                where e.Floorr == floor
                where district.DistrictId == e.District
                group e by e.DistrictNavigation.DistrictName
                into distr_cnt
                select new
                {
                    name = distr_cnt.Key,
                    count = distr_cnt.Count()
                };

            foreach (var e in props)
            {
                Console.WriteLine($"{e.name} - {e.count}");
            }

        }

    }

    public void select_sold_quantity_by_type(int type)
    {
        using (Pks2Context db = new Pks2Context())
        {
            var quantity = from realtor in db.Realtors
                from sell in db.Sellings
                from e in db.EstateObjects
                where e.Types == type && realtor.RealtorId == sell.RealtorId && e.ObjectId == sell.ObjectId
                group realtor by realtor.RealtorName
                into realtor_counter
                select new
                {
                    realtor_counter.Key,
                    count = realtor_counter.Count()
                };
            foreach (var r in quantity)
            {
                Console.WriteLine($"{r.Key} - {r.count}");
            }
        }
    }

    public void select_top_three()
    {
        using (Pks2Context db = new Pks2Context())
        {
            var list = from e in db.EstateObjects
                from district in db.Districts
                where e.District == district.DistrictId
                group e by district.DistrictName
                into objects
                select new
                {
                    district_name = objects.Key,
                    props = (from props in objects
                        orderby props.Price descending, props.Floorr
                        select new
                        {
                            adress = props.Adress,
                            price = props.Price,
                            floor = props.Floorr
                        })
                };
            foreach (var obj in list)
            {
                foreach (var property in (obj.props).Take(3))
                {
                    Console.WriteLine($"{obj.district_name} - {property.adress}, {property.price}, {property.floor}");
                }
            }
        }

    }

    public void select_lucky_years(string rieltor_name, string rieltor_lastname, string rieltor_surname)
    {
        using (Pks2Context db = new Pks2Context())
        {
            var rielt = from selling in db.Sellings
                from realtor in db.Realtors
                where realtor.RealtorName == rieltor_name && realtor.RealtorLastname == rieltor_lastname &&
                      realtor.RealtorSurname == rieltor_surname && selling.RealtorId == realtor.RealtorId
                group selling by selling.SellingDate.Value.Year;
            foreach (var sell in rielt)
            {
                if (sell.Count() > 2)
                {
                    Console.WriteLine(sell.Key);
                }
            }
        }
    }

    // Определить годы, в которых было размещено от 2 до 3 объектов недвижимости.
    public void select_years_when_published(int quantity_min, int quantity_max)
    {
        using (Pks2Context db = new Pks2Context())
        {
            var published = from e in db.EstateObjects
                group e by e.AdDate.Value.Year;
            foreach (var obj in published)
            {
                if (obj.Count() >= quantity_min && obj.Count() <= quantity_max)
                {
                    Console.WriteLine(obj.Key);
                }
            }
        }


    }
    public void lower_then_20_price_difference() {
        using (Pks2Context db = new Pks2Context()) {
            var props = from e in db.EstateObjects
                from selling in db.Sellings
                where e.ObjectId == selling.ObjectId && ((selling.Price - e.Price) / (((selling.Price + e.Price) / 2)) * 100) < 20
                select new
                {
                    e.Adress,
                    e.DistrictNavigation.DistrictName
                };
            foreach (var obj in props)
            {
                Console.WriteLine($"Адрес: {obj.Adress} Название района: {obj.DistrictName}");
            }
        }
    }
    public void select_realtors_with_no_deals(int year) {
        using (Pks2Context db = new Pks2Context()) {
            // Select realtors and their sellings grouped by the realtor's name
            var realtorsWithSellings = from sellings in db.Sellings
                group sellings by sellings.Realtor.RealtorName into realtorGroup
                select new {
                    RealtorName = realtorGroup.Key,
                    SellingsInYear = realtorGroup
                        .Where(s => s.SellingDate.HasValue && s.SellingDate.Value.Year == year)
                };

            // Iterate through the grouped realtors
            foreach (var realtor in realtorsWithSellings) {
                // Check if the realtor has no sellings in the specified year
                if (!realtor.SellingsInYear.Any()) {
                    Console.WriteLine(realtor.RealtorName);
                }
            }
        }
    }
    
    string average_to_text(double? a) {
        double? percent = ((a / 5) * 100);
        if (percent >= 90)
        {
            return "Превосходно";
        }
        else if (percent >= 80)
        {
            return "Очень хорошо";
        }
        else if (percent >= 70) {
            return "Хорошо";
        }
        else if (percent >= 60) {
            return "Удовлетворительно";
        }
        else {
            return "Неудовлетворительно";
        }
            
    }
    
    public void select_marks_stats(string adress) {
        using (Pks2Context db = new Pks2Context()) {
            var grade_list = from grades in db.Grades
                where grades.Object.Adress == adress
                group grades by grades.Param.ParamName into obj
                select new
                {
                    obj.Key,
                    average_mark = (from marks in obj
                        select marks).Average(x => x.Grade1)
                };
            Console.WriteLine("Критерий Средняя оценка Текст");
            foreach (var mark in grade_list)
            {
                Console.WriteLine($"{mark.Key} {mark.average_mark} из 5 {average_to_text(mark.average_mark)}");
            }

        }
    }
}

