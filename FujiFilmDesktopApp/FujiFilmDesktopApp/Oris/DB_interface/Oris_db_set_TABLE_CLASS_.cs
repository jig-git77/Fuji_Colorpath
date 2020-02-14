//using Oris;
//using System;
//using System.Collections.Generic;
//using System.Data.Odbc;
//using System.Reflection;

//namespace Oris.DB_interface
//{
//    public class Oris_db_set<TABLE_CLASS>
//    where TABLE_CLASS : class, new()
//    {
//        private List<Column_description> m_column_list;

//        private List<Db_key_description> m_key_list;

//        private PropertyInfo[] m_properties;

//        private string[] m_db_field_names;

//        private int[] m_db_property_to_column_map;

//        private Type[] m_enum_properties;

//        private Dictionary<string, long>[] m_enum_values_map;

//        private bool[] m_enum_is_flags;

//        private string m_table_name;

//        private Oris_db_context m_db_context;

//        public Oris_db_set(Oris_db_context p_db_context)
//        {
//            this.m_db_context = p_db_context;
//            Column_manager.gen_columns_from_type(typeof(TABLE_CLASS), this.m_column_list, this.m_key_list, out this.m_table_name);
//            this.m_properties = typeof(TABLE_CLASS).GetProperties(BindingFlags.Instance | BindingFlags.Public);
//            this.m_db_field_names = new string[(int)this.m_properties.Length];
//            this.m_enum_properties = new Type[(int)this.m_properties.Length];
//            this.m_enum_values_map = new Dictionary<string, long>[(int)this.m_properties.Length];
//            this.m_enum_is_flags = new bool[(int)this.m_properties.Length];
//            for (int i = 0; i < (int)this.m_properties.Length; i++)
//            {
//                PropertyInfo mProperties = this.m_properties[i];
//                Type propertyType = mProperties.PropertyType;
//                this.m_db_field_names[i] = mProperties.Name;
//                if (this.m_db_field_names[i].StartsWith("m_"))
//                {
//                    this.m_db_field_names[i] = this.m_db_field_names[i].Substring(2);
//                }
//                if (propertyType.BaseType == typeof(Enum))
//                {
//                    this.m_enum_properties[i] = propertyType;
//                }
//                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && propertyType.GetGenericArguments()[0].BaseType == typeof(Enum))
//                {
//                    this.m_enum_properties[i] = propertyType.GetGenericArguments()[0];
//                }
//                if (this.m_enum_properties[i] != null)
//                {
//                    this.m_enum_values_map[i] = new Dictionary<string, long>();
//                    string[] enumNames = this.m_enum_properties[i].GetEnumNames();
//                    Array enumValues = this.m_enum_properties[i].GetEnumValues();
//                    TypeCode typeCode = Type.GetTypeCode(this.m_enum_properties[i]);
//                    for (int j = 0; j < (int)enumNames.Length; j++)
//                    {
//                        string enumValue = Column_manager.gen_odbc_enum_name(enumNames[j]);
//                        this.m_enum_values_map[i][enumValue] = Column_manager.get_enum_value(enumValues, j, typeCode);
//                    }
//                    this.m_enum_is_flags[i] = false;
//                    object[] customAttributes = this.m_enum_properties[i].GetCustomAttributes(false);
//                    for (int k = 0; k < (int)customAttributes.Length; k++)
//                    {
//                        if (customAttributes[k] is FlagsAttribute)
//                        {
//                            this.m_enum_is_flags[i] = true;
//                        }
//                    }
//                }
//            }
//        }

//        public static Oris_db_set<TABLE_CLASS> Create(Oris_db_context p_db_context)
//        {
//            return new Oris_db_set<TABLE_CLASS>(p_db_context);
//        }

//        public TABLE_CLASS find_id(int p_id)
//        {
//            string str;
//            TABLE_CLASS tABLECLAss = default(TABLE_CLASS);
//            bool flag = true;
//            OdbcConnection persistentConnection = Odbc_intf.get_persistent_connection(out str);
//            if (persistentConnection == null)
//            {
//                return default(TABLE_CLASS);
//            }
//            string str1 = string.Concat("SELECT * FROM ", this.m_table_name, " WHERE ID=?");
//            string[] strArrays = new string[] { p_id.ToString() };
//            using (OdbcDataReader odbcDataReader = Odbc_intf.db_query_ex(persistentConnection, str1, ref str, strArrays))
//            {
//                if (!string.IsNullOrEmpty(str))
//                {
//                    flag = false;
//                }
//                else if (odbcDataReader.Read())
//                {
//                    tABLECLAss = this.parse_db_data(odbcDataReader);
//                }
//            }
//            Odbc_intf.return_persistent_connection(persistentConnection);
//            if (flag)
//            {
//                return tABLECLAss;
//            }
//            return default(TABLE_CLASS);
//        }

//        public List<TABLE_CLASS> find_list(string p_where_clause)
//        {
//            string str;
//            List<TABLE_CLASS> tABLECLASSes = new List<TABLE_CLASS>();
//            bool flag = true;
//            OdbcConnection persistentConnection = Odbc_intf.get_persistent_connection(out str);
//            using (OdbcDataReader odbcDataReader = Odbc_intf.db_query_ex(persistentConnection, string.Concat("SELECT * FROM ", this.m_table_name, " ", p_where_clause), ref str, new string[0]))
//            {
//                if (string.IsNullOrEmpty(str))
//                {
//                    while (odbcDataReader.Read())
//                    {
//                        tABLECLASSes.Add(this.parse_db_data(odbcDataReader));
//                    }
//                }
//                else
//                {
//                    flag = false;
//                }
//            }
//            Odbc_intf.return_persistent_connection(persistentConnection);
//            if (flag)
//            {
//                return tABLECLASSes;
//            }
//            return null;
//        }

//        private void gen_property_map(OdbcDataReader p_db_data)
//        {
//            int length = (int)this.m_db_field_names.Length;
//            int fieldCount = p_db_data.FieldCount;
//            this.m_db_property_to_column_map = new int[length];
//            for (int i = 0; i < length; i++)
//            {
//                this.m_db_property_to_column_map[i] = -1;
//            }
//            for (int j = 0; j < fieldCount; j++)
//            {
//                string name = p_db_data.GetName(j);
//                int num = 0;
//                while (num < length)
//                {
//                    if (!name.Equals(this.m_db_field_names[num], StringComparison.OrdinalIgnoreCase))
//                    {
//                        num++;
//                    }
//                    else
//                    {
//                        this.m_db_property_to_column_map[num] = j;
//                        break;
//                    }
//                }
//            }
//        }

//        private TABLE_CLASS parse_db_data(OdbcDataReader p_db_data)
//        {
//            if (this.m_db_property_to_column_map == null)
//            {
//                this.gen_property_map(p_db_data);
//            }
//            TABLE_CLASS tABLECLAss = Activator.CreateInstance<TABLE_CLASS>();
//            for (int i = 0; i < (int)this.m_properties.Length; i++)
//            {
//                PropertyInfo mProperties = this.m_properties[i];
//                Type propertyType = mProperties.PropertyType;
//                int mDbPropertyToColumnMap = this.m_db_property_to_column_map[i];
//                if (mDbPropertyToColumnMap >= 0)
//                {
//                    object item = p_db_data[mDbPropertyToColumnMap];
//                    try
//                    {
//                        if (propertyType == typeof(bool))
//                        {
//                            if (item is short)
//                            {
//                                mProperties.SetValue(tABLECLAss, (short)item != 0, null);
//                            }
//                            else if (item as bool)
//                            {
//                                mProperties.SetValue(tABLECLAss, item, null);
//                            }
//                            else if (!(item is string))
//                            {
//                                mProperties.SetValue(tABLECLAss, false, null);
//                            }
//                            else
//                            {
//                                mProperties.SetValue(tABLECLAss, (string)item == "1", null);
//                            }
//                        }
//                        else if (item is DBNull)
//                        {
//                            mProperties.SetValue(tABLECLAss, null, null);
//                        }
//                        else if (propertyType == typeof(uint) && item is long)
//                        {
//                            mProperties.SetValue(tABLECLAss, (uint)((long)item), null);
//                        }
//                        else if (propertyType == typeof(uint?) && item is long)
//                        {
//                            mProperties.SetValue(tABLECLAss, (uint)((long)item), null);
//                        }
//                        else if (this.m_enum_properties[i] == null)
//                        {
//                            mProperties.SetValue(tABLECLAss, item, null);
//                        }
//                        else
//                        {
//                            this.set_enum_value(tABLECLAss, i, item);
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        string message = exception.Message;
//                    }
//                }
//            }
//            return tABLECLAss;
//        }

//        private void set_enum_value(TABLE_CLASS p_the_object, int p_property_index, object p_db_value)
//        {
//            Type mEnumProperties = this.m_enum_properties[p_property_index];
//            long item = (long)0;
//            if (!string.IsNullOrEmpty((string)p_db_value))
//            {
//                string[] strArrays = ((string)p_db_value).Split(new char[] { ',' });
//                for (int i = 0; i < (int)strArrays.Length; i++)
//                {
//                    item = item | this.m_enum_values_map[p_property_index][strArrays[i]];
//                }
//            }
//            object obj = Enum.ToObject(mEnumProperties, item);
//            this.m_properties[p_property_index].SetValue(p_the_object, obj, null);
//        }
//    }
//}