namespace Game.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public class EnumDescription : Attribute
    {
        private static IDictionary<string, IList<EnumDescription>> EnumDescriptionCache = new Dictionary<string, IList<EnumDescription>>();
        private string m_description;
        private int m_enumRank;
        private FieldInfo m_fieldIno;

        public EnumDescription(string description) : this(description, 5)
        {
        }

        public EnumDescription(string description, int enumRank)
        {
            this.m_description = description;
            this.m_enumRank = enumRank;
        }

        public static bool ExistsEnumValue(Type enumType, int enumValue)
        {
            List<EnumDescription> fieldTexts = GetFieldTexts(enumType) as List<EnumDescription>;
            if (CollectionHelper.IsNullOrEmpty<EnumDescription>(fieldTexts))
            {
                return false;
            }
            return fieldTexts.Exists(item => item.EnumValue == enumValue);
        }

        public static string GetEnumText(Type enumType)
        {
            EnumDescription[] customAttributes = (EnumDescription[]) enumType.GetCustomAttributes(typeof(EnumDescription), false);
            if (customAttributes.Length < 1)
            {
                return string.Empty;
            }
            return customAttributes[0].Description;
        }

        public static string GetFieldText(object enumValue)
        {
            List<EnumDescription> fieldTexts = GetFieldTexts(enumValue.GetType()) as List<EnumDescription>;
            if (CollectionHelper.IsNullOrEmpty<EnumDescription>(fieldTexts))
            {
                return string.Empty;
            }
            EnumDescription description = fieldTexts.Find(item => item.m_fieldIno.Name.Equals(enumValue.ToString()));
            if (description == null)
            {
                return string.Empty;
            }
            return description.Description;
        }

        public static IList<EnumDescription> GetFieldTexts(Type enumType)
        {
            return GetFieldTexts(enumType, SortType.Default);
        }

        public static IList<EnumDescription> GetFieldTexts(Type enumType, SortType sortType)
        {
            if (!EnumDescriptionCache.ContainsKey(enumType.FullName))
            {
                FieldInfo[] fields = enumType.GetFields();
                IList<EnumDescription> list = new List<EnumDescription>();
                foreach (FieldInfo info in fields)
                {
                    object[] customAttributes = info.GetCustomAttributes(typeof(EnumDescription), false);
                    if (customAttributes.Length == 1)
                    {
                        EnumDescription item = (EnumDescription) customAttributes[0];
                        item.m_fieldIno = info;
                        list.Add(item);
                    }
                }
                EnumDescriptionCache.Add(enumType.FullName, list);
            }
            IList<EnumDescription> list2 = EnumDescriptionCache[enumType.FullName];
            if (list2.Count <= 0)
            {
                throw new NotSupportedException("枚举类型[" + enumType.Name + "]未定义属性EnumValueDescription");
            }
            if (sortType != SortType.Default)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    for (int j = i; j < list2.Count; j++)
                    {
                        bool flag = false;
                        switch (sortType)
                        {
                            case SortType.DisplayText:
                                if (string.Compare(list2[i].Description, list2[j].Description) > 0)
                                {
                                    flag = true;
                                }
                                break;

                            case SortType.Rank:
                                if (list2[i].EnumRank > list2[j].EnumRank)
                                {
                                    flag = true;
                                }
                                break;
                        }
                        if (flag)
                        {
                            EnumDescription description2 = list2[i];
                            list2[i] = list2[j];
                            list2[j] = description2;
                        }
                    }
                }
            }
            return list2;
        }

        public string Description
        {
            get
            {
                return this.m_description;
            }
        }

        public int EnumRank
        {
            get
            {
                return this.m_enumRank;
            }
        }

        public int EnumValue
        {
            get
            {
                return (int) this.m_fieldIno.GetValue(null);
            }
        }

        public string FieldName
        {
            get
            {
                return this.m_fieldIno.Name;
            }
        }

        public enum SortType
        {
            Default,
            DisplayText,
            Rank
        }
    }
}

