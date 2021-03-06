﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Root.Core
{
    public class Core
    {
        public Core()
        {
        }

        /// <summary>
        /// Расчет хеш-сигнатуры
        /// </summary>
        /// <param name="inByte">Массив байт</param>
        /// <returns>Хеш-сигнатура</returns>
        public string GetMd5Hash(byte[] inByte)
        {
            Root.Core.Hashing.MD5 md = new Root.Core.Hashing.MD5();
            md.ValueAsByte = inByte;
            return md.FingerPrint;
        }

        /// <summary>
        /// Анализ файла
        /// </summary>
        /// <param name="inByte">Массив байт</param>
        /// <returns>Хеш-сигнатура</returns>
        public string Analysing(byte[] inByte)
        {
            return GetMd5Hash(inByte);
        }
    }
}
