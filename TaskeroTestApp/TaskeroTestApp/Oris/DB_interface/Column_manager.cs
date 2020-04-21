//using Oris;
//using System;
//using System.Collections.Generic;
//using System.Data.Odbc;
//using System.Reflection;

//namespace Oris.DB_interface
//{
//    public class Column_manager
//    {
//        public Column_manager()
//        {
//        }

//        private static void analyze_enum_column(Type p_prop_type, object p_default_value, bool p_no_default_attr_seen, Column_description p_col_desc)
//        {
//            bool flag = false;
//            object[] customAttributes = p_prop_type.GetCustomAttributes(false);
//            for (int i = 0; i < (int)customAttributes.Length; i++)
//            {
//                if (customAttributes[i] is FlagsAttribute)
//                {
//                    flag = true;
//                }
//            }
//            string[] enumNames = p_prop_type.GetEnumNames();
//            Array enumValues = p_prop_type.GetEnumValues();
//            TypeCode typeCode = Type.GetTypeCode(p_prop_type);
//            long num = (long)0;
//            if (p_default_value != null)
//            {
//                if (p_default_value.GetType() == p_prop_type)
//                {
//                    string str = p_default_value.ToString();
//                    if (flag)
//                    {
//                        int num1 = 0;
//                        while (num1 < (int)enumNames.Length)
//                        {
//                            if (str != enumNames[num1])
//                            {
//                                num1++;
//                            }
//                            else
//                            {
//                                long enumValue = Column_manager.get_enum_value(enumValues, num1, typeCode);
//                                if (enumValue == (long)0 || (enumValue & enumValue - (long)1) == (long)0)
//                                {
//                                    break;
//                                }
//                                num = enumValue;
//                                break;
//                            }
//                        }
//                    }
//                    if (num == (long)0)
//                    {
//                        str = Column_manager.gen_odbc_enum_name(str);
//                        p_col_desc.m_default_value = str;
//                    }
//                }
//            }
//            else if (p_no_default_attr_seen)
//            {
//                p_col_desc.m_default_value = null;
//            }
//            else if (!flag)
//            {
//                p_col_desc.m_default_value = Column_manager.gen_odbc_enum_name(enumNames[0]);
//            }
//            else
//            {
//                p_col_desc.m_default_value = "";
//            }
//            if (!flag)
//            {
//                p_col_desc.m_datatype = Column_type.k_mysql_enum;
//            }
//            else
//            {
//                p_col_desc.m_datatype = Column_type.k_mysql_set;
//            }
//            List<string> strs = new List<string>();
//            for (int j = 0; j < (int)enumNames.Length; j++)
//            {
//                string str1 = Column_manager.gen_odbc_enum_name(enumNames[j]);
//                if (!flag)
//                {
//                    strs.Add(string.Concat("'", str1, "'"));
//                }
//                else
//                {
//                    long enumValue1 = Column_manager.get_enum_value(enumValues, j, typeCode);
//                    if (enumValue1 != (long)0 && (enumValue1 & enumValue1 - (long)1) == (long)0)
//                    {
//                        strs.Add(string.Concat("'", str1, "'"));
//                        if ((enumValue1 & num) != (long)0)
//                        {
//                            if (!string.IsNullOrEmpty(p_col_desc.m_default_value))
//                            {
//                                Column_description pColDesc = p_col_desc;
//                                pColDesc.m_default_value = string.Concat(pColDesc.m_default_value, ",", str1);
//                            }
//                            else
//                            {
//                                p_col_desc.m_default_value = str1;
//                            }
//                        }
//                    }
//                }
//            }
//            p_col_desc.m_set_enum_values = string.Join(",", strs);
//        }

//        private static void compare_column_list(List<Column_description> p_wanted_columns, List<Column_description> p_actual_columns, List<Column_description> p_changed_columns, List<Column_description> p_removed_columns, List<Column_description> p_new_columns)
//        {
//            bool[] flagArray = new bool[p_actual_columns.Count];
//            for (int i = 0; i < p_wanted_columns.Count; i++)
//            {
//                Column_description item = p_wanted_columns[i];
//                Column_description columnDescription = null;
//                int num = 0;
//                while (num < p_actual_columns.Count)
//                {
//                    if (!string.Equals(item.m_name, p_actual_columns[num].m_name, StringComparison.OrdinalIgnoreCase))
//                    {
//                        num++;
//                    }
//                    else
//                    {
//                        flagArray[num] = true;
//                        columnDescription = p_actual_columns[num];
//                        break;
//                    }
//                }
//                if (columnDescription == null)
//                {
//                    p_new_columns.Add(item);
//                }
//                else
//                {
//                    bool flag = false;
//                    if (item.m_datatype != columnDescription.m_datatype)
//                    {
//                        flag = true;
//                    }
//                    if (!flag && item.m_allow_null != columnDescription.m_allow_null)
//                    {
//                        flag = true;
//                    }
//                    if (!flag && item.m_datatype == Column_type.k_mysql_set && item.m_set_enum_values != columnDescription.m_set_enum_values)
//                    {
//                        flag = true;
//                    }
//                    if (!flag && item.m_datatype == Column_type.k_mysql_enum && item.m_set_enum_values != columnDescription.m_set_enum_values)
//                    {
//                        flag = true;
//                    }
//                    if (!flag && item.m_datatype == Column_type.k_string && item.m_column_size != columnDescription.m_column_size)
//                    {
//                        flag = true;
//                    }
//                    if (!flag && item.m_default_value != columnDescription.m_default_value)
//                    {
//                        flag = true;
//                    }
//                    if (flag)
//                    {
//                        p_changed_columns.Add(item);
//                    }
//                }
//            }
//            for (int j = 0; j < (int)flagArray.Length; j++)
//            {
//                if (!flagArray[j])
//                {
//                    p_removed_columns.Add(p_actual_columns[j]);
//                }
//            }
//        }

//        public static void compare_key_list(List<Db_key_description> p_wanted_keys, List<Db_key_description> p_actual_keys, List<Db_key_description> p_changed_keys, List<Db_key_description> p_removed_keys, List<Db_key_description> p_new_keys)
//        {
//            bool[] flagArray = new bool[p_actual_keys.Count];
//            for (int i = 0; i < p_wanted_keys.Count; i++)
//            {
//                Db_key_description item = p_wanted_keys[i];
//                if (item.m_key_type != Db_key_type.k_foreign_key)
//                {
//                    Db_key_description dbKeyDescription = null;
//                    int num = 0;
//                    while (num < p_actual_keys.Count)
//                    {
//                        if (p_actual_keys[num].m_key_name == item.m_key_name)
//                        {
//                            flagArray[num] = true;
//                            dbKeyDescription = p_actual_keys[num];
//                            break;
//                        }
//                        else if (item.m_key_type == Db_key_type.k_index && p_actual_keys[num].m_key_type == Db_key_type.k_index && p_actual_keys[num].m_key_name == item.m_index_name)
//                        {
//                            flagArray[num] = true;
//                            dbKeyDescription = p_actual_keys[num];
//                            break;
//                        }
//                        else if (item.m_key_type != Db_key_type.k_unique_key || p_actual_keys[num].m_key_type != Db_key_type.k_unique_key || !(p_actual_keys[num].m_key_name == item.m_index_name))
//                        {
//                            num++;
//                        }
//                        else
//                        {
//                            flagArray[num] = true;
//                            dbKeyDescription = p_actual_keys[num];
//                            break;
//                        }
//                    }
//                    if (dbKeyDescription == null)
//                    {
//                        p_new_keys.Add(item);
//                    }
//                    else
//                    {
//                        bool flag = false;
//                        if (item.m_key_type != dbKeyDescription.m_key_type)
//                        {
//                            flag = true;
//                        }
//                        else if (item.m_key_type == Db_key_type.k_primary_key)
//                        {
//                            if (item.m_index_columns != dbKeyDescription.m_index_columns)
//                            {
//                                flag = true;
//                            }
//                        }
//                        else if (item.m_key_type == Db_key_type.k_unique_key)
//                        {
//                            if (item.m_index_columns != dbKeyDescription.m_index_columns)
//                            {
//                                flag = true;
//                            }
//                        }
//                        else if (item.m_key_type == Db_key_type.k_index && item.m_index_columns != dbKeyDescription.m_index_columns)
//                        {
//                            flag = true;
//                        }
//                        if (flag)
//                        {
//                            p_changed_keys.Add(item);
//                        }
//                    }
//                }
//            }
//            for (int j = 0; j < (int)flagArray.Length; j++)
//            {
//                if (!flagArray[j])
//                {
//                    p_removed_keys.Add(p_actual_keys[j]);
//                }
//            }
//        }

//        private static void fixup_new_constraints(List<Db_key_description> p_wanted_key_list, List<Column_description> p_new_columns, List<Db_key_description> p_new_keys)
//        {
//            for (int i = 0; i < p_wanted_key_list.Count; i++)
//            {
//                if (p_wanted_key_list[i].m_key_type == Db_key_type.k_foreign_key && p_wanted_key_list[i].m_foreign_on_delete != Fk_constraint.k_nothing && p_wanted_key_list[i].m_foreign_on_update != Fk_constraint.k_nothing)
//                {
//                    for (int j = 0; j < p_new_columns.Count; j++)
//                    {
//                        if (p_wanted_key_list[i].m_index_columns == p_new_columns[j].m_name && !p_new_keys.Contains(p_wanted_key_list[i]))
//                        {
//                            p_new_keys.Add(p_wanted_key_list[i]);
//                        }
//                    }
//                }
//            }
//        }

//        public static void gen_columns_from_type(Type p_record_type, List<Column_description> p_column_list, List<Db_key_description> p_key_list, out string p_table_name)
//        {
//            bool flag;
//            p_table_name = null;
//            object[] customAttributes = p_record_type.GetCustomAttributes(false);
//            for (int i = 0; i < (int)customAttributes.Length; i++)
//            {
//                TableAttribute tableAttribute = customAttributes[i] as TableAttribute;
//                if (tableAttribute != null)
//                {
//                    p_table_name = tableAttribute.Name;
//                }
//            }
//            PropertyInfo[] properties = p_record_type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
//            for (int j = 0; j < (int)properties.Length; j++)
//            {
//                PropertyInfo propertyInfo = properties[j];
//                Column_description columnDescription = new Column_description();
//                Type propertyType = propertyInfo.PropertyType;
//                string name = propertyInfo.Name;
//                object[] objArray = propertyInfo.GetCustomAttributes(false);
//                if (name.StartsWith("m_"))
//                {
//                    name = name.Substring(2);
//                }
//                columnDescription.m_name = name;
//                uint maximumLength = 0;
//                bool flag1 = true;
//                bool flag2 = false;
//                bool flag3 = false;
//                bool flag4 = false;
//                bool flag5 = false;
//                bool flag6 = false;
//                object @default = null;
//                Db_key_description dbKeyDescription = null;
//                Db_key_description dbKeyDescription1 = null;
//                Db_key_description order = null;
//                object[] objArray1 = objArray;
//                for (int k = 0; k < (int)objArray1.Length; k++)
//                {
//                    object obj = objArray1[k];
//                    StringLengthAttribute stringLengthAttribute = obj as StringLengthAttribute;
//                    if (stringLengthAttribute != null)
//                    {
//                        maximumLength = stringLengthAttribute.MaximumLength;
//                    }
//                    if (obj is TextAttribute)
//                    {
//                        flag2 = true;
//                    }
//                    if (obj is LongTextAttribute)
//                    {
//                        flag3 = true;
//                    }
//                    if (obj is RequiredAttribute)
//                    {
//                        flag1 = false;
//                    }
//                    DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
//                    if (defaultValueAttribute != null)
//                    {
//                        @default = defaultValueAttribute.Default;
//                    }
//                    if (obj is NoDefaultAttribute)
//                    {
//                        flag4 = true;
//                    }
//                    if (obj is TimestampAttribute)
//                    {
//                        flag5 = true;
//                    }
//                    if (obj is DateAttribute)
//                    {
//                        flag6 = true;
//                    }
//                    KeyAttribute keyAttribute = obj as KeyAttribute;
//                    if (keyAttribute != null)
//                    {
//                        dbKeyDescription = new Db_key_description()
//                        {
//                            m_key_type = Db_key_type.k_primary_key
//                        };
//                        if (keyAttribute.Order != 0)
//                        {
//                            dbKeyDescription.m_key_name = name;
//                        }
//                        else
//                        {
//                            dbKeyDescription.m_key_name = "PRIMARY";
//                            dbKeyDescription.m_index_columns = name;
//                        }
//                        dbKeyDescription.m_index_name = "PRIMARY";
//                        dbKeyDescription.m_index_order = keyAttribute.Order;
//                        p_key_list.Add(dbKeyDescription);
//                    }
//                    ForeignKeyAttribute foreignKeyAttribute = obj as ForeignKeyAttribute;
//                    if (foreignKeyAttribute != null)
//                    {
//                        dbKeyDescription1 = new Db_key_description()
//                        {
//                            m_key_name = name,
//                            m_key_type = Db_key_type.k_foreign_key,
//                            m_index_name = foreignKeyAttribute.Name,
//                            m_index_columns = name,
//                            m_foreign_table = foreignKeyAttribute.table,
//                            m_foreign_column = foreignKeyAttribute.column,
//                            m_foreign_on_update = foreignKeyAttribute.on_update,
//                            m_foreign_on_delete = foreignKeyAttribute.on_delete
//                        };
//                        p_key_list.Add(dbKeyDescription1);
//                    }
//                    IndexAttribute indexAttribute = obj as IndexAttribute;
//                    if (indexAttribute != null)
//                    {
//                        order = new Db_key_description()
//                        {
//                            m_key_name = name
//                        };
//                        if (!indexAttribute.is_unique)
//                        {
//                            order.m_key_type = Db_key_type.k_index;
//                        }
//                        else
//                        {
//                            order.m_key_type = Db_key_type.k_unique_key;
//                        }
//                        if (indexAttribute.Order < 0)
//                        {
//                            order.m_index_columns = name;
//                        }
//                        order.m_index_name = indexAttribute.Name;
//                        order.m_index_order = indexAttribute.Order;
//                        p_key_list.Add(order);
//                    }
//                }
//                if (dbKeyDescription1 != null && (dbKeyDescription1.m_foreign_on_delete != Fk_constraint.k_nothing || dbKeyDescription1.m_foreign_on_update != Fk_constraint.k_nothing) && (order == null || order.m_index_order > 1) && (dbKeyDescription == null || dbKeyDescription.m_index_order != 1))
//                {
//                    order = new Db_key_description()
//                    {
//                        m_key_name = dbKeyDescription1.m_key_name,
//                        m_key_type = Db_key_type.k_index,
//                        m_index_columns = name,
//                        m_index_name = dbKeyDescription1.m_index_name,
//                        m_index_order = -1
//                    };
//                    p_key_list.Add(order);
//                }
//                if (propertyType != typeof(DateTime))
//                {
//                    bool flag7 = propertyType != typeof(DateTime?);
//                }
//                if (propertyType == typeof(int))
//                {
//                    if (@default != null)
//                    {
//                        columnDescription.m_default_value = @default as string;
//                    }
//                    else if (!flag4)
//                    {
//                        if (dbKeyDescription != null && dbKeyDescription.m_index_order == 0)
//                        {
//                            columnDescription.m_default_value = "auto_increment";
//                        }
//                        else if (dbKeyDescription == null || dbKeyDescription.m_index_order != 1)
//                        {
//                            columnDescription.m_default_value = "0";
//                        }
//                        else
//                        {
//                            columnDescription.m_default_value = "auto_increment";
//                        }
//                    }
//                    columnDescription.m_datatype = Column_type.k_int32;
//                    flag1 = false;
//                }
//                else if (propertyType == typeof(int?))
//                {
//                    if (@default != null)
//                    {
//                        columnDescription.m_default_value = @default as string;
//                    }
//                    columnDescription.m_datatype = Column_type.k_int32;
//                    flag1 = true;
//                }
//                else if (propertyType == typeof(uint))
//                {
//                    if (@default != null)
//                    {
//                        columnDescription.m_default_value = @default as string;
//                    }
//                    else if (!flag4)
//                    {
//                        columnDescription.m_default_value = "0";
//                    }
//                    columnDescription.m_datatype = Column_type.k_uint32;
//                    flag1 = false;
//                }
//                else if (propertyType == typeof(uint?))
//                {
//                    columnDescription.m_datatype = Column_type.k_uint32;
//                    flag1 = true;
//                }
//                else if (propertyType == typeof(byte))
//                {
//                    if (@default != null)
//                    {
//                        columnDescription.m_default_value = @default as string;
//                    }
//                    else if (!flag4)
//                    {
//                        columnDescription.m_default_value = "0";
//                    }
//                    columnDescription.m_datatype = Column_type.k_uint8;
//                    flag1 = false;
//                }
//                else if (propertyType == typeof(byte?))
//                {
//                    columnDescription.m_datatype = Column_type.k_uint8;
//                    flag1 = true;
//                }
//                else if (propertyType == typeof(sbyte))
//                {
//                    if (@default != null)
//                    {
//                        columnDescription.m_default_value = @default as string;
//                    }
//                    else if (!flag4)
//                    {
//                        columnDescription.m_default_value = "0";
//                    }
//                    columnDescription.m_datatype = Column_type.k_int8;
//                    flag1 = false;
//                }
//                else if (propertyType == typeof(sbyte?))
//                {
//                    columnDescription.m_datatype = Column_type.k_int8;
//                    flag1 = true;
//                }
//                else if (propertyType == typeof(float))
//                {
//                    if (@default != null)
//                    {
//                        columnDescription.m_default_value = @default as string;
//                    }
//                    else if (!flag4)
//                    {
//                        columnDescription.m_default_value = "0";
//                    }
//                    columnDescription.m_datatype = Column_type.k_float;
//                    flag1 = false;
//                }
//                else if (propertyType == typeof(float?))
//                {
//                    if (@default != null)
//                    {
//                        columnDescription.m_default_value = @default as string;
//                    }
//                    columnDescription.m_datatype = Column_type.k_float;
//                    flag1 = true;
//                }
//                else if (propertyType == typeof(string))
//                {
//                    if (!flag2)
//                    {
//                        if (@default != null)
//                        {
//                            columnDescription.m_default_value = @default as string;
//                        }
//                        else if (!flag1 && !flag4)
//                        {
//                            columnDescription.m_default_value = "";
//                        }
//                        if (!flag3)
//                        {
//                            columnDescription.m_column_size = maximumLength;
//                        }
//                        else
//                        {
//                            columnDescription.m_column_size = -1;
//                        }
//                    }
//                    else
//                    {
//                        columnDescription.m_column_size = 65535;
//                    }
//                    columnDescription.m_datatype = Column_type.k_string;
//                }
//                else if (propertyType == typeof(DateTime))
//                {
//                    if (@default != null)
//                    {
//                        columnDescription.m_default_value = @default as string;
//                    }
//                    if (columnDescription.m_default_value != "CURRENT_TIMESTAMP")
//                    {
//                        if (!flag4)
//                        {
//                            columnDescription.m_default_value = "0000-00-00 00:00:00";
//                        }
//                        if (flag5)
//                        {
//                            columnDescription.m_datatype = Column_type.k_mysql_timestamp;
//                        }
//                        else if (!flag6)
//                        {
//                            columnDescription.m_datatype = Column_type.k_date_time;
//                        }
//                        else
//                        {
//                            columnDescription.m_datatype = Column_type.k_date;
//                        }
//                    }
//                    else
//                    {
//                        columnDescription.m_datatype = Column_type.k_mysql_timestamp;
//                    }
//                    flag1 = false;
//                }
//                else if (propertyType == typeof(DateTime?))
//                {
//                    if (flag5)
//                    {
//                        columnDescription.m_datatype = Column_type.k_mysql_timestamp;
//                    }
//                    else if (!flag6)
//                    {
//                        columnDescription.m_datatype = Column_type.k_date_time;
//                    }
//                    else
//                    {
//                        columnDescription.m_datatype = Column_type.k_date;
//                    }
//                    flag1 = true;
//                }
//                else if (propertyType == typeof(bool))
//                {
//                    if (@default != null && (bool)@default)
//                    {
//                        columnDescription.m_default_value = "1";
//                    }
//                    columnDescription.m_datatype = Column_type.k_mysql_enum;
//                    columnDescription.m_set_enum_values = "'1'";
//                    flag1 = true;
//                }
//                else if (propertyType.BaseType == typeof(Enum))
//                {
//                    Column_manager.analyze_enum_column(propertyType, @default, flag4, columnDescription);
//                    flag1 = false;
//                }
//                else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && propertyType.GetGenericArguments()[0].BaseType == typeof(Enum))
//                {
//                    Column_manager.analyze_enum_column(propertyType.GetGenericArguments()[0], @default, flag4, columnDescription);
//                    columnDescription.m_default_value = null;
//                    flag1 = true;
//                }
//                else if (propertyType == typeof(byte[]))
//                {
//                    columnDescription.m_datatype = Column_type.k_byte_array;
//                }
//                columnDescription.m_allow_null = flag1;
//                p_column_list.Add(columnDescription);
//            }
//            for (int l = 0; l < p_key_list.Count; l++)
//            {
//                if (p_key_list[l].m_index_order > 0)
//                {
//                    Db_key_description item = p_key_list[l];
//                    string mIndexName = item.m_index_name;
//                    int num = 1;
//                    do
//                    {
//                        flag = false;
//                        int num1 = l;
//                        while (num1 < p_key_list.Count)
//                        {
//                            if (!(p_key_list[num1].m_index_name == mIndexName) || p_key_list[num1].m_index_order != num)
//                            {
//                                num1++;
//                            }
//                            else
//                            {
//                                if (num != 1)
//                                {
//                                    Db_key_description dbKeyDescription2 = item;
//                                    dbKeyDescription2.m_index_columns = string.Concat(dbKeyDescription2.m_index_columns, ",", p_key_list[num1].m_key_name);
//                                }
//                                else
//                                {
//                                    item.m_index_columns = p_key_list[num1].m_key_name;
//                                }
//                                if (l == num1)
//                                {
//                                    item.m_key_name = mIndexName;
//                                }
//                                else
//                                {
//                                    if (item.m_key_type == Db_key_type.k_index && p_key_list[num1].m_key_type == Db_key_type.k_unique_key)
//                                    {
//                                        item.m_key_type = Db_key_type.k_unique_key;
//                                    }
//                                    p_key_list.RemoveAt(num1);
//                                }
//                                num++;
//                                flag = true;
//                                goto Label0;
//                            }
//                        }
//                    Label0:
//                    }
//                    while (flag);
//                    if (mIndexName == "PRIMARY")
//                    {
//                        item.m_key_name = "PRIMARY";
//                    }
//                    int num2 = l + 1;
//                    while (num2 < p_key_list.Count)
//                    {
//                        num2++;
//                    }
//                }
//            }
//        }

//        private static string gen_new_odbc_columns(string p_table_name, bool p_create_table, List<Column_description> p_changed_columns, List<Column_description> p_removed_columns, List<Column_description> p_new_columns, List<Db_key_description> p_changed_keys, List<Db_key_description> p_removed_keys, List<Db_key_description> p_new_keys)
//        {
//            string str;
//            string str1 = null;
//            if (p_changed_columns != null && p_changed_columns.Count != 0 && !p_create_table)
//            {
//                for (int i = 0; i < p_changed_columns.Count; i++)
//                {
//                    string str2 = string.Concat("CHANGE COLUMN ", p_changed_columns[i].m_name, " ");
//                    str2 = string.Concat(str2, Column_manager.gen_odbc_column_definition(p_changed_columns[i]));
//                    if (str1 != null)
//                    {
//                        str1 = string.Concat(str1, ",\r\n");
//                    }
//                    str1 = string.Concat(str1, str2);
//                }
//            }
//            if (p_new_columns != null)
//            {
//                for (int j = 0; j < p_new_columns.Count; j++)
//                {
//                    string str3 = Column_manager.gen_odbc_column_definition(p_new_columns[j]);
//                    if (!p_create_table)
//                    {
//                        str3 = string.Concat("ADD COLUMN ", str3);
//                    }
//                    if (str1 != null)
//                    {
//                        str1 = string.Concat(str1, ",\r\n");
//                    }
//                    str1 = string.Concat(str1, str3);
//                }
//            }
//            if (p_removed_columns != null && p_removed_columns.Count != 0 && !p_create_table)
//            {
//                for (int k = 0; k < p_removed_columns.Count; k++)
//                {
//                    string str4 = string.Concat("DROP COLUMN ", p_removed_columns[k].m_name);
//                    if (str1 != null)
//                    {
//                        str1 = string.Concat(str1, ",\r\n");
//                    }
//                    str1 = string.Concat(str1, str4);
//                }
//            }
//            string str5 = null;
//            if (p_changed_keys != null && p_changed_keys.Count != 0 && !p_create_table)
//            {
//                str5 = Column_manager.gen_odbc_drop_keys(str5, p_changed_keys);
//                str5 = Column_manager.gen_odbc_new_keys(str5, p_create_table, p_changed_keys);
//            }
//            if (p_new_keys != null && p_new_keys.Count != 0)
//            {
//                str5 = Column_manager.gen_odbc_new_keys(str5, p_create_table, p_new_keys);
//            }
//            if (str5 != null && str1 != null)
//            {
//                str1 = string.Concat(str1, ",\r\n");
//            }
//            if (!p_create_table)
//            {
//                string[] pTableName = new string[] { "ALTER TABLE ", p_table_name, " \r\n", str1, str5, "\r\n;" };
//                str = string.Concat(pTableName);
//            }
//            else
//            {
//                string[] strArrays = new string[] { "CREATE TABLE ", p_table_name, " (\r\n", str1, str5, "\r\n);" };
//                str = string.Concat(strArrays);
//            }
//            return str;
//        }

//        public static string gen_odbc_column_definition(Column_description p_column)
//        {
//            string mName = p_column.m_name;
//            string mDefaultValue = p_column.m_default_value;
//            bool flag = false;
//            if (p_column.m_datatype == Column_type.k_int32)
//            {
//                mName = string.Concat(mName, " INTEGER");
//            }
//            else if (p_column.m_datatype == Column_type.k_uint32)
//            {
//                mName = string.Concat(mName, " INTEGER UNSIGNED");
//            }
//            else if (p_column.m_datatype == Column_type.k_int8)
//            {
//                mName = string.Concat(mName, " TINYINT");
//            }
//            else if (p_column.m_datatype == Column_type.k_uint8)
//            {
//                mName = string.Concat(mName, " TINYINT UNSIGNED");
//            }
//            else if (p_column.m_datatype == Column_type.k_float)
//            {
//                mName = string.Concat(mName, " FLOAT");
//                flag = true;
//            }
//            else if (p_column.m_datatype == Column_type.k_mysql_set)
//            {
//                mName = string.Concat(mName, " SET(", p_column.m_set_enum_values, ")");
//                flag = true;
//            }
//            else if (p_column.m_datatype == Column_type.k_mysql_enum)
//            {
//                mName = string.Concat(mName, " ENUM(", p_column.m_set_enum_values, ")");
//                flag = true;
//            }
//            else if (p_column.m_datatype == Column_type.k_mysql_timestamp)
//            {
//                mName = string.Concat(mName, " TIMESTAMP");
//                if (mDefaultValue != "CURRENT_TIMESTAMP")
//                {
//                    flag = true;
//                }
//                else
//                {
//                    mDefaultValue = string.Concat(mDefaultValue, " ON UPDATE CURRENT_TIMESTAMP");
//                }
//            }
//            else if (p_column.m_datatype == Column_type.k_date_time)
//            {
//                mName = string.Concat(mName, " DATETIME");
//                flag = true;
//            }
//            else if (p_column.m_datatype == Column_type.k_date)
//            {
//                mName = string.Concat(mName, " DATE");
//                flag = true;
//            }
//            else if (p_column.m_datatype == Column_type.k_string)
//            {
//                if (p_column.m_column_size == 65535 || p_column.m_column_size == 0)
//                {
//                    mName = string.Concat(mName, " TEXT");
//                }
//                else
//                {
//                    mName = (p_column.m_column_size != -1 ? string.Concat(mName, " VARCHAR(", p_column.m_column_size.ToString(), ")") : string.Concat(mName, " LONGTEXT"));
//                }
//                flag = true;
//            }
//            else if (p_column.m_datatype == Column_type.k_byte_array)
//            {
//                mName = string.Concat(mName, " BLOB");
//                flag = true;
//            }
//            mName = (!p_column.m_allow_null ? string.Concat(mName, " NOT NULL") : string.Concat(mName, " NULL"));
//            if (mDefaultValue != null)
//            {
//                if (flag)
//                {
//                    mDefaultValue = string.Concat("'", mDefaultValue, "'");
//                }
//                if (mDefaultValue != "auto_increment")
//                {
//                    mName = string.Concat(mName, " DEFAULT");
//                }
//                mName = string.Concat(mName, " ", mDefaultValue);
//            }
//            return mName;
//        }

//        private static string gen_odbc_drop_keys(string p_keys_string, List<Db_key_description> p_removed_keys)
//        {
//            string pKeysString = p_keys_string;
//            Db_key_description item = null;
//            for (int i = 0; i < p_removed_keys.Count; i++)
//            {
//                if (p_removed_keys[i].m_key_type == Db_key_type.k_primary_key)
//                {
//                    item = p_removed_keys[i];
//                }
//            }
//            if (item != null)
//            {
//                if (pKeysString != null)
//                {
//                    pKeysString = string.Concat(pKeysString, ",\r\n");
//                }
//                pKeysString = string.Concat(pKeysString, "DROP PRIMARY KEY");
//            }
//            for (int j = 0; j < p_removed_keys.Count; j++)
//            {
//                if (p_removed_keys[j].m_key_type == Db_key_type.k_unique_key || p_removed_keys[j].m_key_type == Db_key_type.k_index)
//                {
//                    if (pKeysString != null)
//                    {
//                        pKeysString = string.Concat(pKeysString, ",\r\n");
//                    }
//                    pKeysString = (string.IsNullOrEmpty(p_removed_keys[j].m_index_name) ? string.Concat(pKeysString, "DROP INDEX ", p_removed_keys[j].m_index_columns) : string.Concat(pKeysString, "DROP INDEX ", p_removed_keys[j].m_index_name));
//                }
//                else if (p_removed_keys[j].m_key_type == Db_key_type.k_foreign_key && p_removed_keys[j].m_foreign_on_delete != Fk_constraint.k_nothing && p_removed_keys[j].m_foreign_on_update != Fk_constraint.k_nothing)
//                {
//                    if (pKeysString != null)
//                    {
//                        pKeysString = string.Concat(pKeysString, ",\r\n");
//                    }
//                    pKeysString = string.Concat(pKeysString, "DROP FOREIGN KEY ", p_removed_keys[j].m_index_name);
//                }
//            }
//            return pKeysString;
//        }

//        public static string gen_odbc_enum_name(string p_enum_name)
//        {
//            string pEnumName = p_enum_name;
//            if (pEnumName.StartsWith("k_"))
//            {
//                pEnumName = pEnumName.Substring(2);
//            }
//            int num = pEnumName.IndexOf("__");
//            if (num > 0)
//            {
//                pEnumName = string.Concat(pEnumName.Substring(0, num), '-', pEnumName.Substring(num + 2));
//            }
//            return pEnumName;
//        }

//        private static string gen_odbc_new_keys(string p_keys_string, bool p_create_table, List<Db_key_description> p_new_keys)
//        {
//            string pKeysString = p_keys_string;
//            Db_key_description item = null;
//            for (int i = 0; i < p_new_keys.Count; i++)
//            {
//                if (p_new_keys[i].m_key_type == Db_key_type.k_primary_key)
//                {
//                    item = p_new_keys[i];
//                }
//            }
//            if (item != null && p_create_table)
//            {
//                if (pKeysString != null)
//                {
//                    pKeysString = string.Concat(pKeysString, ",\r\n");
//                }
//                if (!p_create_table)
//                {
//                    pKeysString = string.Concat(pKeysString, "ADD ");
//                }
//                pKeysString = string.Concat(pKeysString, "PRIMARY KEY (", item.m_index_columns, ")");
//            }
//            for (int j = 0; j < p_new_keys.Count; j++)
//            {
//                if (p_new_keys[j].m_key_type == Db_key_type.k_unique_key)
//                {
//                    if (pKeysString != null)
//                    {
//                        pKeysString = string.Concat(pKeysString, ",\r\n");
//                    }
//                    if (!p_create_table)
//                    {
//                        pKeysString = string.Concat(pKeysString, "ADD ");
//                    }
//                    if (string.IsNullOrEmpty(p_new_keys[j].m_index_name))
//                    {
//                        string str = pKeysString;
//                        string[] mIndexColumns = new string[] { str, "UNIQUE INDEX ", p_new_keys[j].m_index_columns, " (", p_new_keys[j].m_index_columns, ")" };
//                        pKeysString = string.Concat(mIndexColumns);
//                    }
//                    else
//                    {
//                        string str1 = pKeysString;
//                        string[] mIndexName = new string[] { str1, "UNIQUE INDEX ", p_new_keys[j].m_index_name, " (", p_new_keys[j].m_index_columns, ")" };
//                        pKeysString = string.Concat(mIndexName);
//                    }
//                }
//            }
//            for (int k = 0; k < p_new_keys.Count; k++)
//            {
//                if (p_new_keys[k].m_key_type == Db_key_type.k_index)
//                {
//                    if (pKeysString != null)
//                    {
//                        pKeysString = string.Concat(pKeysString, ",\r\n");
//                    }
//                    if (!p_create_table)
//                    {
//                        pKeysString = string.Concat(pKeysString, "ADD ");
//                    }
//                    if (string.IsNullOrEmpty(p_new_keys[k].m_index_name))
//                    {
//                        string str2 = pKeysString;
//                        string[] strArrays = new string[] { str2, "INDEX ", p_new_keys[k].m_index_columns, " (", p_new_keys[k].m_index_columns, ")" };
//                        pKeysString = string.Concat(strArrays);
//                    }
//                    else
//                    {
//                        string str3 = pKeysString;
//                        string[] mIndexName1 = new string[] { str3, "INDEX ", p_new_keys[k].m_index_name, " (", p_new_keys[k].m_index_columns, ")" };
//                        pKeysString = string.Concat(mIndexName1);
//                    }
//                }
//            }
//            for (int l = 0; l < p_new_keys.Count; l++)
//            {
//                if (p_new_keys[l].m_key_type == Db_key_type.k_foreign_key && p_new_keys[l].m_foreign_on_delete != Fk_constraint.k_nothing && p_new_keys[l].m_foreign_on_update != Fk_constraint.k_nothing)
//                {
//                    if (pKeysString != null)
//                    {
//                        pKeysString = string.Concat(pKeysString, ",\r\n");
//                    }
//                    if (!p_create_table)
//                    {
//                        pKeysString = string.Concat(pKeysString, "ADD ");
//                    }
//                    string str4 = pKeysString;
//                    string[] strArrays1 = new string[] { str4, "CONSTRAINT ", p_new_keys[l].m_index_name, " FOREIGN KEY (", p_new_keys[l].m_index_columns, ") REFERENCES ", p_new_keys[l].m_foreign_table, " (", p_new_keys[l].m_foreign_column, ")" };
//                    pKeysString = string.Concat(strArrays1);
//                    switch (p_new_keys[l].m_foreign_on_delete)
//                    {
//                        case Fk_constraint.k_nothing:
//                        {
//                            switch (p_new_keys[l].m_foreign_on_update)
//                            {
//                                case Fk_constraint.k_cascade:
//                                {
//                                    pKeysString = string.Concat(pKeysString, " ON UPDATE CASCADE");
//                                    break;
//                                }
//                            }
//                            break;
//                        }
//                        case Fk_constraint.k_cascade:
//                        {
//                            pKeysString = string.Concat(pKeysString, " ON DELETE CASCADE");
//                            goto case Fk_constraint.k_nothing;
//                        }
//                        case Fk_constraint.k_restrict:
//                        {
//                            pKeysString = string.Concat(pKeysString, " ON DELETE RESTRICT");
//                            goto case Fk_constraint.k_nothing;
//                        }
//                        case Fk_constraint.k_set_null:
//                        {
//                            pKeysString = string.Concat(pKeysString, " ON DELETE SET NULL");
//                            goto case Fk_constraint.k_nothing;
//                        }
//                        default:
//                        {
//                            goto case Fk_constraint.k_nothing;
//                        }
//                    }
//                }
//            }
//            return pKeysString;
//        }

//        public static long get_enum_value(Array p_enum_values, int p_index, TypeCode p_enum_int_type)
//        {
//            if (p_enum_int_type == TypeCode.Int64)
//            {
//                return (long)p_enum_values.GetValue(p_index);
//            }
//            return (long)((int)p_enum_values.GetValue(p_index));
//        }

//        public static bool update_table(OdbcConnection p_odbc_connection, Type p_record_type, out string p_table_name, out string p_create_alter_code, out bool p_needs_seeding, bool p_gen_create_code)
//        {
//            p_create_alter_code = null;
//            p_needs_seeding = false;
//            List<Column_description> columnDescriptions = new List<Column_description>();
//            List<Db_key_description> dbKeyDescriptions = new List<Db_key_description>();
//            Column_manager.gen_columns_from_type(p_record_type, columnDescriptions, dbKeyDescriptions, out p_table_name);
//            List<Column_description> columnDescriptions1 = new List<Column_description>();
//            if (!Odbc_intf.get_column_list(p_odbc_connection, p_table_name, columnDescriptions1))
//            {
//                return false;
//            }
//            if (p_gen_create_code || columnDescriptions1.Count == 0)
//            {
//                p_needs_seeding = true;
//                p_create_alter_code = Column_manager.gen_new_odbc_columns(p_table_name, true, null, null, columnDescriptions, null, null, dbKeyDescriptions);
//                return true;
//            }
//            List<Db_key_description> dbKeyDescriptions1 = new List<Db_key_description>();
//            if (!Odbc_intf.get_db_key_list(p_odbc_connection, p_table_name, dbKeyDescriptions1))
//            {
//                return false;
//            }
//            List<Column_description> columnDescriptions2 = new List<Column_description>();
//            List<Column_description> columnDescriptions3 = new List<Column_description>();
//            List<Column_description> columnDescriptions4 = new List<Column_description>();
//            Column_manager.compare_column_list(columnDescriptions, columnDescriptions1, columnDescriptions2, columnDescriptions3, columnDescriptions4);
//            List<Db_key_description> dbKeyDescriptions2 = new List<Db_key_description>();
//            List<Db_key_description> dbKeyDescriptions3 = new List<Db_key_description>();
//            List<Db_key_description> dbKeyDescriptions4 = new List<Db_key_description>();
//            Column_manager.compare_key_list(dbKeyDescriptions, dbKeyDescriptions1, dbKeyDescriptions2, dbKeyDescriptions3, dbKeyDescriptions4);
//            Column_manager.fixup_new_constraints(dbKeyDescriptions, columnDescriptions4, dbKeyDescriptions4);
//            if (columnDescriptions2.Count != 0 || columnDescriptions3.Count != 0 || columnDescriptions4.Count != 0 || dbKeyDescriptions4.Count != 0)
//            {
//                p_create_alter_code = Column_manager.gen_new_odbc_columns(p_table_name, false, columnDescriptions2, columnDescriptions3, columnDescriptions4, dbKeyDescriptions2, null, dbKeyDescriptions4);
//            }
//            return true;
//        }
//    }
//}