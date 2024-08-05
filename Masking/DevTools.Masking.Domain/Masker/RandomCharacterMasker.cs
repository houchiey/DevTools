using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Masking.Domain.Masker
{
    internal class RandomCharacterMasker : IMasker
    {
        private readonly ImmutableArray<char> _randomChars = ImmutableArray.Create(
            '*', '+', '@', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
            'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            'v', 'w', 'x', 'y', 'z');

        private readonly MaskingOptions _options;

        internal RandomCharacterMasker(MaskingOptions options)
        {
            _options = options;
        }

        public string Mask(string original)
        {
            if (string.IsNullOrEmpty(original) 
                || original.Length < _options.MaskingCharCount)
            {
                return original;
            }

            var maskFromIndex = GetMaskStartIndex(original.Length);

            return string.Concat(
                original[..maskFromIndex],
                CreateMasking(),
                original[(maskFromIndex + _options.MaskingCharCount)..]);

            
        }

        private string CreateMasking()
        {
            // ランダムな文字からマスキング文字数分ピックして配列化
            var maskingCharArray = Enumerable.Range(1, _options.MaskingCharCount)
                .Select(i => {
                    var random = new Random().Next(_randomChars.Length);
                    return _randomChars[random];
                }).ToArray();

            
            return new string(maskingCharArray);
        }

        private int GetMaskStartIndex(int strLength) 
        { 
            if(strLength == _options.MaskingCharCount)
            {
                return 0;
            }

            return new Random().Next(strLength - _options.MaskingCharCount);
        }
    }
}
