/*
 * Copyright (c) 2018 Demerzel Solutions Limited
 * This file is part of the Nethermind library.
 *
 * The Nethermind library is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * The Nethermind library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with the Nethermind. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Numerics;
using Nethermind.Core.Crypto;
using Nethermind.Core.Extensions;
using Nethermind.Dirichlet.Numerics;

namespace Nethermind.Core.Encoding
{
    public class AccountDecoder : IRlpDecoder<Account>
    {
        public Account Decode(Rlp.DecoderContext context, RlpBehaviors rlpBehaviors = RlpBehaviors.None)
        {
            context.ReadSequenceLength();
            //long checkValue = context.ReadSequenceLength() + context.Position;

            UInt256 nonce = context.DecodeUInt256();
            UInt256 balance = context.DecodeUInt256();
            Keccak storageRoot = context.DecodeKeccak();
            Keccak codeHash = context.DecodeKeccak();
            Account account = new Account(nonce, balance, storageRoot, codeHash);

            //if (!rlpBehaviors.HasFlag(RlpBehaviors.AllowExtraData))
            //{
            //    context.Check(checkValue);
            //}

            return account;
        }

        public Rlp Encode(Account item, RlpBehaviors rlpBehaviors = RlpBehaviors.None)
        {
            Rlp rlp = Rlp.Encode(
                Rlp.Encode(item.Nonce),
                Rlp.Encode((BigInteger)item.Balance),
                Rlp.Encode(item.StorageRoot),
                Rlp.Encode(item.CodeHash));
            
            Rlp rlp2 = Rlp.Encode(
                Rlp.Encode(item.Nonce),
                Rlp.Encode(item.Balance),
                Rlp.Encode(item.StorageRoot),
                Rlp.Encode(item.CodeHash));

            if (!Bytes.UnsafeCompare(rlp.Bytes, rlp2.Bytes))
            {
                throw new Exception();
            }
            
                return Rlp.Encode(
                    Rlp.Encode(item.Nonce),
                    Rlp.Encode(item.Balance),
                    Rlp.Encode(item.StorageRoot),
                    Rlp.Encode(item.CodeHash));
        }
    }
}