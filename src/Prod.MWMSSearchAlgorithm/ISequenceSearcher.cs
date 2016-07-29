﻿/**
 * ISequenceSearcher.cs
 * 
 * PLessPP - Copyright (C) 2016
 * Andrea Tino, Constantin Daniil, Jeroen Rietveld, 
 * Liansheng Hua, Nikola Kukrika, Sam van Lieshout
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace PLessPP.AI.MWMSSearchAlgorithm
{
    using System;

    using PLessPP.Data;

    /// <summary>
    /// 
    /// </summary>
    public interface ISequenceSearcher
    {
        /// <summary>
        /// Searches a particular sequence inside another one.
        /// </summary>
        /// <param name="sequence">The <see cref="Sequence"/> to search into.</param>
        /// <param name="results"></param>
        void Search(Sequence sequence, out object results);
    }
}