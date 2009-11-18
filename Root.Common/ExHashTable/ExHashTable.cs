using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Root.Classes.Compress;

namespace Root.Common
{
    public class ExHashTable
    {
        public delegate void OnOK(string msg, string cmt);
        public event OnOK OK;

        public delegate void OnMakeError(string Error);
        public event OnMakeError MakeError;

        //Расширение файла обновления
        private const string ext = ".csu";

        //Базовые сигнатуры (по которым идет поиск Сканера)
        public Hashtable ht_base;

        //Новые сигнатуры (которых нет в базовых)
        public Hashtable ht_new = new Hashtable(6000);

        //Глобальный показатель ошибки
        public bool error_transaction = false;

        /// <summary>
        /// Счетчик вызова функции AddNewSignature
        /// </summary>
        int counter_new_ht = 0;
        public Int32 CountNewHt
        {
            get { return counter_new_ht; }
            set { counter_new_ht = value; }
        }

        public ExHashTable()
        {

        }

        /// <summary>
        /// Конструктор. Создает хеш-таблицу базы данных из файла
        /// </summary>
        /// <param name="db_hash">Путь к файлу базы данных</param>
        public void LoadBases(string db_hash)
        {
            try
            {
                using (FileStream fStream = File.Open(db_hash, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter outFormatter = new BinaryFormatter();
                    ht_base = (Hashtable)outFormatter.Deserialize(fStream);
                }
                if (OK != null)
                {
                    OK.Invoke("Базы проверены", "ОК. Записей: " + ht_base.Count.ToString());
                }
            }
            catch (Exception ex)
            {
                error_transaction = true;
                if (MakeError != null)
                {
                    MakeError.Invoke(ex.Message);
                }
            }
        }

        /// <summary>
        /// Создает файл основного хранилища БД.
        /// </summary>
        /// <param name="db_hash">Путь к файлу базы данных</param>
        public void CreateDB(string db_hash)
        {
            try
            {
                using (FileStream fStream = File.Open(db_hash, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    Hashtable ht_tmp = new Hashtable(300000);
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fStream, ht_tmp);
                }
                if (OK != null)
                {
                    OK.Invoke("База создана", "Новая глобальная база создана");
                }
            }
            catch (Exception ex)
            {
                error_transaction = true;
                if (MakeError != null)
                {
                    MakeError.Invoke(ex.Message);
                }
            }
        }

        /// <summary>
        /// Создает файл основного хранилища БД и загружает в основную хеш-таблицу.
        /// </summary>
        /// <param name="db_hash">Путь к файлу базы данных</param>
        public void CreateDBAndLoadHTB(string db_hash)
        {
            try
            {
                using (FileStream fStream = File.Open(db_hash, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    Hashtable ht_tmp = new Hashtable(300000);
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fStream, ht_tmp);
                }
                using (FileStream fStream = File.Open(db_hash, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter outFormatter = new BinaryFormatter();
                    ht_base = (Hashtable)outFormatter.Deserialize(fStream);
                }
                if (OK != null)
                {
                    OK.Invoke("База создана", "Общая база создана. Хеш-таблица сформирована");
                }
            }
            catch (Exception ex)
            {
                error_transaction = true;
                if (MakeError != null)
                {
                    MakeError.Invoke(ex.Message);
                }
            }
        }

        int prev_new_cnt = 0;
        /// <summary>
        /// Добавляет новую уникальную сигнатуру к временной хеш-таблице базы данных. Не сохраняет БД.
        /// </summary>
        /// <param name="signature">Сигнатура</param>
        /// <returns>Успех/Отказ (сигнатура уже есть или ошибка)</returns>
        public bool AddNewHtSign(string signature, bool write_ok_evnt)
        {
            bool return_val = false;
            try
            {
                counter_new_ht++;
                if (counter_new_ht == 1)
                {
                    prev_new_cnt = ht_new.Count;
                }

                if (!ht_base.ContainsKey(signature) &&
                    !ht_new.ContainsKey(signature))
                {
                    ht_new.Add(signature, string.Empty);
                    return_val = true;
                }
                else
                {
                    return_val = false;
                }
                if (OK != null && write_ok_evnt)
                {
                    int div = ht_new.Count - prev_new_cnt;
                    OK.Invoke("Новая сигнатура принята", "Проверено за сессию: " + counter_new_ht.ToString()
                                + ", новых: " + div.ToString() + ", всего новых: " + ht_new.Count.ToString());
                }
            }
            catch (Exception ex)
            {
                error_transaction = true;
                if (MakeError != null)
                {
                    MakeError.Invoke(ex.Message);
                }
            }

            return return_val;
        }

        /// <summary>
        /// Добавляет все временные сигнатуры в хеш-таблицу базы данных
        /// </summary>
        public void AddNewHtSignsToHtBase(int prev_cnt)
        {
            try
            {
                foreach (object key in ht_new.Keys)
                {
                    if (!ht_base.ContainsKey(key))
                    {
                        ht_base.Add(key, string.Empty);
                    }
                }
                if (OK != null)
                {
                    int div = ht_base.Count - prev_cnt;
                    OK.Invoke("Сигнатуры добавлены в базу", "Новые сигнатуры отобраны и добавлены в базу (всего: " + div.ToString() + ")");
                }
            }
            catch(Exception ex)
            { 
                error_transaction = true;
                if (MakeError != null)
                {
                    MakeError.Invoke(ex.Message);
                }
            }
        }

        public bool CheckSign(object sign)
        {
            if (ht_base.ContainsKey(sign))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Сохраняет файл базы данных с последней версией хеш-таблицы
        /// </summary>
        /// <param name="db_hash">Путь к файлу базы данных</param>
        /// <returns>Успех/Отказ</returns>
        public bool SaveHtSighToDB(string db_hash)
        {
            try
            {
                using (FileStream fStream = File.Open(db_hash, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fStream, ht_base);
                }
                if (OK != null)
                {
                    OK.Invoke("База сохранена", "Хеш-таблица сохранена в базу данных (записей: " + ht_base.Count.ToString() + ")");
                }
                return true;
            }
            catch (Exception ex)
            {
                error_transaction = true;
                if (MakeError != null)
                {
                    MakeError.Invoke(ex.Message);
                }
                return false;
            }
        }

        /// <summary>
        /// Сохраняет файл новых уникальных сигнатур, добавленных функцией AddNewHtSign ДО (!!!) добавления их в общую хеш-таблицу
        /// </summary>
        /// <param name="db_update">Путь к файлу новых уникальных сигнатур</param>
        /// <param name="pb">ProgressBar</param>
        /// <returns>Успех/Отказ</returns>
        public bool SaveNewHtSighToDB_upd(string db_update, ProgressBar pb)
        {
            try
            {
                using (FileStream fStream = File.Open(db_update, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fStream, ht_new);
                }
                new Pack(pb).PackingFile(db_update, db_update + ext, true);
                if (OK != null)
                {
                    OK.Invoke("Файл обновления создан", "ОК. Записей: " + ht_new.Count.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                error_transaction = true;
                if (MakeError != null)
                {
                    MakeError.Invoke(ex.Message);
                }
                return false;
            }
        }

        /// <summary>
        /// Добавляет в основную хеш-таблицу базы данных новые уникальные сигнатуы из файла
        /// (прежде сохраненные функцией SaveNewHtSighToDB_upd)
        /// </summary>
        /// <param name="db_update">Путь к файлу новых уникальных сигнатур</param>
        /// <param name="pb">ProgressBar</param>
        /// <returns>Успех/Отказ</returns>
        public bool LoadNewHtSighInDB_upd(string db_update, ProgressBar pb)
        {
            try
            {
                int first_cnt = ht_base.Count;
                Hashtable ht_update;
                new UnPack(pb).UnPackingFile(db_update, db_update + ext, false);
                using (FileStream fStream = File.Open(db_update + ext, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter outFormatter = new BinaryFormatter();
                    ht_update = (Hashtable)outFormatter.Deserialize(fStream);
                }

                foreach (object key in ht_update.Keys)
                {
                    if (!ht_base.ContainsKey(key))
                    {
                        ht_base.Add(key, string.Empty);
                    }
                }
                File.Delete(db_update + ext);
                if (OK != null)
                {
                    int div = ht_base.Count-first_cnt;
                    OK.Invoke("Обновление", "Новые сигнатуры добавлены в хеш-таблицу (всего: " + div.ToString() + ")");
                }
                return true;
            }
            catch (Exception ex)
            {
                File.Delete(db_update + ext);
                error_transaction = true;
                if (MakeError != null)
                {
                    MakeError.Invoke(ex.Message);
                }
                return false;
            }
        }
    }
}
