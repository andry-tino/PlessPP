/**
 * FirstFoundPairedBandProvider.cs
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

namespace PLessPP.Device.BandGestureFlowRetrieval
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Band;

    /// <summary>
    /// 
    /// </summary>
    public class FirstFoundPairedBandProvider : IPairedBandProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IBandInfo GetPairedBand()
        {
            IBandInfo[] pairedBands = null;

            try
            {
                // Get the list of Microsoft Bands paired to the phone.
                Task<IBandInfo[]> getBandsTask = this.RetrievePairedBandsAsync();

                getBandsTask.Wait();
                pairedBands = getBandsTask.Result;
            }
            catch (Exception ex)
            {
                throw new BandNotFoundException("No band found!", ex);
            }

            if (pairedBands != null && pairedBands.Length < 1)
            {
                throw new BandNotFoundException("No band found even though call to API succeeded!");
            }

            return pairedBands[0];
        }

        private async Task<IBandInfo[]> RetrievePairedBandsAsync()
        {
            IBandInfo[] pairedBands = null;

            try
            {
                // Get the list of Microsoft Bands paired to the phone.
                pairedBands = await BandClientManager.Instance.GetBandsAsync();
            }
            catch (Exception ex)
            {
                throw new BandNotFoundException("No band found!", ex);
            }

            if (pairedBands != null && pairedBands.Length < 1)
            {
                return null;
            }

            return pairedBands;
        }
    }
}
