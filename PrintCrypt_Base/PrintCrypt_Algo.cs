using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintCrypt_Base
{
    public class PrintCrypt_Algo
    {
        public static int ENT_KEY_BLOCK_SIZE = 256;
        public static int ENT_KEY_SIZE = 512;
        public static void entyty_domixcode(byte[] inkey1, byte[] inkey2, byte[] buf, int len, ref byte[] inoutbuf)
        {
            byte[] inbuf = buf;
            byte[] outinbuf = new byte[ENT_KEY_BLOCK_SIZE];
            int j = 0;
            int c1, c2, mask;
            int l;
            int r;
            for (int i = 0; i < len; i += 2)
            {
                c1 = inbuf[i];
                c2 = inbuf[i + 1];
                mask = inkey1[j++];
                l = (c1 & (~mask));
                r = (c2 & (mask));

                outinbuf[i] = (byte)(l | r);
                l = (c2 & (~mask));
                r = (c1 & (mask));

                outinbuf[i + 1] = (byte)(l | r);
            }

            inbuf = outinbuf;
            j = len - 1;
            for (int i = 0; i < len; i++)
            {
                c1 = inbuf[i];
                c2 = inbuf[inkey2[i]];
                mask = inkey1[j--];
                l = (c1 & (~mask));
                r = (c2 & (mask));
                inoutbuf[i] = (byte)(l | r);
                l = (c2 & (~mask));
                r = (c1 & (mask));
                inoutbuf[(uint)inkey2[i]] = (byte)(l | r);
            }
            j = len / 3;
            for (int i = 1; i < len - 1; i += 2)
            {
                c1 = inbuf[i];
                c2 = inbuf[i + 1];
                mask = inkey1[j++];

                l = (c1 & (~mask));
                r = (c2 & (mask));
                inoutbuf[i] = (byte)(l | r);
                l = (c2 & (~mask));
                r = (c1 & (mask));
                inoutbuf[i + 1] = (byte)(l | r);
            }
            c1 = inbuf[len - 1];
            c2 = inbuf[0];
            mask = inkey1[j];
            l = (c1 & (~mask));
            r = (c2 & (mask));
            inoutbuf[len - 1] = (byte)(l | r);

            l = (c2 & (~mask));
            r = (c1 & (mask));
            inoutbuf[0] = (byte)(l | r);

        }
        public static void entyty_doxcode(byte[] key1, byte[] key2, byte[] buf, int len, ref byte[] outbuf)
        {
            for (int i = 0; i < len; i++)
            {
                outbuf[(uint)key2[i]] = (byte)((int)buf[i] ^ (int)key1[(uint)key2[i]]);
            }
        }

        public static void entyty_demixcode(byte[] inkey1, byte[] inkey2, byte[] buf, int len, ref byte[] inoutbuf)
        {
            byte[] inbuf = buf;
            byte[] outinbuf = new byte[ENT_KEY_BLOCK_SIZE];
            int j = 0;
            int c1, c2, mask;
            int l;
            int r;
            j = len / 3;
            for (int i = 1; i < len - 1; i += 2)
            {
                c1 = inbuf[i];
                c2 = inbuf[i + 1];
                mask = inkey1[j++];
                l = (c1 & (~mask));
                r = (c2 & (mask));

                outinbuf[i] = (byte)(l | r);
                l = (c2 & (~mask));
                r = (c1 & (mask));

                outinbuf[i + 1] = (byte)(l | r);
            }
            c1 = inbuf[len - 1];
            c2 = inbuf[0];
            mask = inkey1[j];
            l = (c1 & (~mask));
            r = (c2 & (mask));
            outinbuf[len - 1] = (byte)(l | r);

            l = (c2 & (~mask));
            r = (c1 & (mask));
            outinbuf[0] = (byte)(l | r);


            inbuf = outinbuf;
            j = 0;
            for (int i = len - 1; i >= 0; i--)
            {
                c1 = inbuf[i];
                c2 = inbuf[(uint)inkey2[i]];
                mask = inkey1[j++];
                inoutbuf[i] = (byte)((c1 & (~mask)) | (c2 & mask));
                inoutbuf[(uint)inkey2[i]] = (byte)((c2 & (~mask)) | (c1 & mask));
            }

            j = 0;
            for (int i = 0; i < len; i += 2)
            {
                c1 = inbuf[i];
                c2 = inbuf[i + 1];
                mask = inkey1[j++];
                c1 = inbuf[i];
                c2 = inbuf[i + 1];

                l = (c1 & (~mask));
                r = (c2 & (mask));

                inoutbuf[i] = (byte)(l | r);
                l = (c2 & (~mask));
                r = (c1 & (mask));

                inoutbuf[i + 1] = (byte)(l | r);
            }

        }


        public static void entyty_dexcode(byte[] key1, byte[] key2, byte[] buf, int len, ref byte[] outbuf)
        {
            for (int i = 0; i < len; i++)
            {
                outbuf[i] = (byte)((int)buf[(uint)key2[i]] ^ (int)key1[(uint)key2[i]]);
            }
        }

        public static int entyty_decrypt512(byte[] inbuf, byte[] inkey, ref byte[] outbuf, int len)
        {
            byte[] buf1 = new byte[ENT_KEY_BLOCK_SIZE];
            byte[] buf2 = new byte[ENT_KEY_BLOCK_SIZE];
            byte[] key = new byte[ENT_KEY_SIZE];
            Array.Copy(inkey, key, ENT_KEY_SIZE);
            if (len < 8)
            {
                Array.Copy(inbuf, buf1, len);
                for (int i = 0; i < len; i++)
                {
                    outbuf[i] = (byte)((int)key[53 + i] ^ (int)key[312 + i] ^ (int)buf1[i]);
                }
            }
            else
            {
                int fullblk = len / ENT_KEY_BLOCK_SIZE;
                byte[] key1 = new byte[ENT_KEY_BLOCK_SIZE];
                byte[] key2 = new byte[ENT_KEY_BLOCK_SIZE];
                Array.Copy(inkey, key1, ENT_KEY_BLOCK_SIZE);
                Array.Copy(inkey, ENT_KEY_BLOCK_SIZE, key2, 0, ENT_KEY_BLOCK_SIZE);
                if (fullblk == 0)
                {
                    Array.Copy(inbuf, buf1, len);
                    int ln1 = len;
                    if (len % 2 != 0)
                    {
                        outbuf[len - 1] = (byte)((int)key[35] ^ (int)key[350] ^ (int)buf1[len - 1]);
                        ln1--;
                    }
                    byte[] newkey = new byte[ENT_KEY_BLOCK_SIZE];
                    int ii = 0;
                    for (int i = 0; i < ENT_KEY_BLOCK_SIZE; i++)
                    {
                        if ((uint)key2[i] < (uint)ln1)
                        {
                            newkey[ii++] = key2[i];
                        }
                    }
                    entyty_demixcode(key1, newkey, buf1, ln1, ref buf2);
                    entyty_dexcode(key1, newkey, buf2, ln1, ref buf1);
                    entyty_demixcode(key1, newkey, buf1, ln1, ref outbuf);
                }
                else
                {
                    int restlen = len % ENT_KEY_BLOCK_SIZE;

                    if (restlen != 0)
                    {
                        fullblk--;
                    }
                    int keyj = 135;
                    int curr = 0;
                    for (int i = 0; i < fullblk; i++)
                    {
                        Array.Copy(inbuf, curr, buf1, 0, ENT_KEY_BLOCK_SIZE);
                        entyty_demixcode(key1, key2, buf1, ENT_KEY_BLOCK_SIZE, ref buf2);
                        entyty_dexcode(key1, key2, buf2, ENT_KEY_BLOCK_SIZE, ref buf1);
                        entyty_demixcode(key1, key2, buf1, ENT_KEY_BLOCK_SIZE, ref buf2);
                        Array.Copy(buf2, 0, outbuf, curr, ENT_KEY_BLOCK_SIZE);
                        curr += ENT_KEY_BLOCK_SIZE;
                        byte chh = key[keyj++];

                        if (keyj > 255)
                            keyj = 0;

                        for (int j = 0; j < ENT_KEY_SIZE; j++)
                        {
                            key[j] += chh;
                        }
                    }
                    if (restlen != 0)
                    {
                        int len1 = (ENT_KEY_BLOCK_SIZE + restlen) / 2;
                        int ln1 = len1;

                        Array.Copy(inbuf, curr, buf1, 0, len1);
                        if (len1 % 2 != 0)
                        {
                            buf2[len1 - 1] = (byte)((int)key[35] ^ (int)key[350] ^ (int)buf1[len1 - 1]);
                            ln1--;
                        }
                        byte[] newkey = new byte[ENT_KEY_BLOCK_SIZE];
                        int ii = 0;
                        for (int i = 0; i < ENT_KEY_BLOCK_SIZE; i++)
                        {
                            if ((uint)key2[i] < (uint)ln1)
                            {
                                newkey[ii++] = key2[i];
                            }
                        }
                        entyty_demixcode(key1, newkey, buf1, ln1, ref buf2);
                        entyty_dexcode(key1, newkey, buf2, ln1, ref buf1);
                        entyty_demixcode(key1, newkey, buf1, ln1, ref buf2);
                        Array.Copy(buf2, 0, outbuf, curr, len1);

                        len1 = ENT_KEY_BLOCK_SIZE + restlen - len1;
                        byte ch = key[keyj];
                        for (int j = 0; j < ENT_KEY_BLOCK_SIZE; j++)
                        {
                            key[j] += ch;
                        }
                        curr += len1;
                        ln1 = len1;
                        Array.Copy(inbuf, curr, buf1, 0, len1);
                        if (len1 % 2 != 0)
                        {
                            buf2[len1 - 1] = (byte)((int)key[34] ^ (int)key[351] ^ (int)buf1[len1 - 1]);
                            ln1--;
                        }
                        ii = 0;
                        for (int i = 0; i < ENT_KEY_BLOCK_SIZE; i++)
                        {
                            if ((uint)key2[i] < (uint)ln1)
                            {
                                newkey[ii++] = key2[i];
                            }
                        }
                        entyty_demixcode(key1, newkey, buf1, ln1, ref buf2);
                        entyty_dexcode(key1, newkey, buf2, ln1, ref buf1);
                        entyty_demixcode(key1, newkey, buf1, ln1, ref buf2);
                        Array.Copy(buf2, 0, outbuf, curr, len1);
                    }
                }
            }
            return 0;
        }
        public static int entyty_encrypt512(byte[] inbuf, byte[] inkey, ref byte[] outbuf, int len)
        {
            byte[] buf1 = new byte[ENT_KEY_BLOCK_SIZE];
            byte[] buf2 = new byte[ENT_KEY_BLOCK_SIZE];
            byte[] key = new byte[ENT_KEY_SIZE];
            Array.Copy(inkey, key, ENT_KEY_SIZE);

            if (len < 8)
            {

                for (int i = 0; i < len; i++)
                {
                    buf1[i] = (byte)((int)key[53 + i] ^ (int)key[312 + i] ^ (int)inbuf[i]);
                }
                Array.Copy(buf1, outbuf, len);
            }
            else
            {
                int fullblk = len / ENT_KEY_BLOCK_SIZE;
                byte[] key1 = new byte[ENT_KEY_BLOCK_SIZE];
                byte[] key2 = new byte[ENT_KEY_BLOCK_SIZE];
                Array.Copy(inkey, key1, ENT_KEY_BLOCK_SIZE);
                Array.Copy(inkey, ENT_KEY_BLOCK_SIZE, key2, 0, ENT_KEY_BLOCK_SIZE);
                if (fullblk == 0)
                {
                    Array.Copy(inbuf, buf1, len);
                    int ln1 = len;
                    if (len % 2 != 0)
                    {
                        buf2[len - 1] = (byte)((int)key[35] ^ (int)key[350] ^ (int)inbuf[len - 1]);
                        ln1--;
                    }
                    byte[] newkey = new byte[ENT_KEY_BLOCK_SIZE];
                    int ii = 0;
                    for (int i = 0; i < ENT_KEY_BLOCK_SIZE; i++)
                    {
                        if ((uint)key2[i] < (uint)ln1)
                        {
                            newkey[ii++] = key2[i];
                        }
                    }
                    entyty_domixcode(key1, newkey, buf1, ln1, ref buf2);
                    entyty_doxcode(key1, newkey, buf2, ln1, ref buf1);
                    entyty_domixcode(key1, newkey, buf1, ln1, ref buf2);
                    Array.Copy(buf2, outbuf, len);

                }
                else
                {
                    int restlen = len % ENT_KEY_BLOCK_SIZE;

                    if (restlen != 0)
                    {
                        fullblk--;
                    }
                    int curr = 0;

                    int keyj = 135;
                    for (int i = 0; i < fullblk; i++)
                    {
                        Array.Copy(inbuf, curr, buf1, 0, ENT_KEY_BLOCK_SIZE);
                        entyty_domixcode(key1, key2, buf1, ENT_KEY_BLOCK_SIZE, ref buf2);
                        entyty_doxcode(key1, key2, buf2, ENT_KEY_BLOCK_SIZE, ref buf1);
                        entyty_domixcode(key1, key2, buf1, ENT_KEY_BLOCK_SIZE, ref buf2);

                        Array.Copy(buf2, 0, outbuf, curr, ENT_KEY_BLOCK_SIZE);

                        curr += ENT_KEY_BLOCK_SIZE;
                        byte chh = key[keyj++];
                        if (keyj > 255)
                            keyj = 0;
                        for (int j = 0; j < ENT_KEY_SIZE; j++)
                        {
                            key[j] += chh;
                        }
                    }
                    if (restlen != 0)
                    {
                        int len1 = (ENT_KEY_BLOCK_SIZE + restlen) / 2;
                        int ln1 = len1;
                        Array.Copy(inbuf, curr, buf1, 0, len1);
                        if (len1 % 2 != 0)
                        {
                            buf2[len1 - 1] = (byte)((int)key[35] ^ (int)key[350] ^ (int)inbuf[curr + len1 - 1]);
                            ln1--;
                        }
                        byte[] newkey = new byte[ENT_KEY_BLOCK_SIZE];
                        int ii = 0;
                        for (int i = 0; i < ENT_KEY_BLOCK_SIZE; i++)
                        {
                            if ((uint)key2[i] < (uint)ln1)
                            {
                                newkey[ii++] = key2[i];
                            }
                        }
                        entyty_domixcode(key1, newkey, buf1, ln1, ref buf2);
                        entyty_doxcode(key1, newkey, buf2, ln1, ref buf1);
                        entyty_domixcode(key1, newkey, buf1, ln1, ref buf2);
                        Array.Copy(buf2, 0, outbuf, curr, len1);
                        curr += len1;
                        len1 = ENT_KEY_BLOCK_SIZE + restlen - len1;
                        byte ch = key[keyj];
                        for (int j = 0; j < ENT_KEY_BLOCK_SIZE; j++)
                        {
                            key[j] += ch;
                        }
                        Array.Copy(inbuf, curr, buf1, 0, len1);
                        ln1 = len1;
                        if (len1 % 2 != 0)
                        {
                            buf2[len1 - 1] = (byte)((int)key[34] ^ (int)key[351] ^ (int)inbuf[curr + len1 - 1]);
                            ln1--;
                        }
                        ii = 0;
                        for (int i = 0; i < ENT_KEY_BLOCK_SIZE; i++)
                        {
                            if ((uint)key2[i] < (uint)ln1)
                            {
                                newkey[ii++] = key2[i];
                            }
                        }
                        entyty_domixcode(key1, newkey, buf1, ln1, ref buf2);
                        entyty_doxcode(key1, newkey, buf2, ln1, ref buf1);
                        entyty_domixcode(key1, newkey, buf1, ln1, ref buf2);
                        Array.Copy(buf2, 0, outbuf, curr, len1);
                    }
                }
            }
            return 0;
        }

        static int[] expandsig = new int[256] {75,64,232,199,51,7,106,172,73,211,153,169,199,211,119,9,
                                            120,56,111,174,42,138,180,239,70,132,230,127,41,76,76,47,
                                            49,208,162,198,15,225,19,149,203,94,180,62,193,177,62,57,
                                            80,48,53,160,252,61,134,166,145,116,67,180,6,97,147,98,
                                            79,186,152,131,244,85,233,80,140,45,95,158,109,84,27,164,
                                            30,134,59,119,14,76,207,86,172,229,185,133,212,37,158,233,
                                            45,111,188,38,142,89,182,18,235,64,131,32,105,8,10,164,
                                            182,251,74,80,133,253,193,101,242,136,132,96,209,195,210,178,
                                            95,212,159,132,156,187,137,165,172,235,140,103,163,70,17,27,
                                            59,126,230,76,207,164,16,96,190,113,168,217,40,254,147,6,
                                            1,239,212,106,93,249,30,207,236,181,110,157,24,141,50,61,
                                            7,226,104,200,36,187,197,197,54,234,207,113,244,76,55,11,
                                            9,218,115,101,70,133,237,65,7,145,131,106,170,163,44,196,
                                            76,225,83,197,116,95,150,92,131,47,195,33,57,210,48,19,
                                            169,111,61,147,202,63,87,70,197,32,102,211,97,101,107,179,
                                            227,154,15,4,8,77,1,74,143,125,56,57,159,160,18,204};

        public static byte[] expand_256key_to_512(byte[] key256)
        {
            byte[] key512 = new byte[512];
            bool[] bkey = new bool[256];

            for (int i = 0; i < 256; i++)
            {
                bkey[i] = false;
            }


            for (int i = 0; i < 256; i++)
            {
                key512[i] = key256[i];
                int k = (i) ^ key512[expandsig[i]];

                while (bkey[k])
                {
                    if (k == ENT_KEY_BLOCK_SIZE - 1)
                        k = 0;
                    else
                        k++;
                }
                key512[i + 256] = (byte)k;
                bkey[k] = true;

            }
            return key512;
        }


        public static byte[] create_key(int sz1, int sz2, int[,] xx, int[,] yy, int[,] tt)
        {
            int outsize = sz1;
            byte[] resa = new byte[outsize];


            int[,] rr = new int[sz1, sz2];
            for (int j = 0; j < sz2; j++)
            {
                for (int i = 0; i < sz1; i++)
                {
                    rr[i, j] = (int)Math.Sqrt((double)xx[i, j] * (double)xx[i, j] + (double)yy[i, j] * (double)yy[i, j]);
                    rr[i, j] = (rr[i, j] % sz1) ^ (tt[i, j] % sz1);
                }
            }
            int[] resm = new int[outsize];
            Random r = new Random(tt[sz1 / 3, sz2 / 3]);
            int l = rr[sz1 / 2, sz2 / 2];
            for (int i = 0; i < l; i++)
            {
                r.Next(1, sz1);
            }

            for (int i = 0; i < sz1; i++)
            {
                resm[i] = r.Next(1, sz1);
            }

            int[] resi = new int[sz1];
            for (int i = 0; i < sz1; i++)
            {
                resi[i] = rr[i, 0];
            }

            for (int i = 0; i < sz1; i++)
            {
                for (int j = 1; j < sz2; j++)
                {
                    resi[i] = rr[resi[i], j];
                }
            }

            bool[] resb = new bool[sz1];

            for (int i = 0; i < sz1; i++)
            {
                resb[i] = true;
            }
            for (int i = 0; i < sz1; i++)
            {
                if (resb[resi[i]])
                {
                    resa[i] = (byte)resm[resi[i]];
                    resb[resi[i]] = false;
                }
                else
                {
                    int kk = resi[i] + 1;
                    while (true)
                    {
                        if (kk == sz1)
                        {
                            kk = 0;
                        }
                        if (resb[kk])
                        {
                            resa[i] = (byte)resm[kk];
                            resb[kk] = false;
                            break;
                        }
                        else
                        {
                            kk++;
                        }
                    }
                }

            }

            return resa;
        }

        public static string genrate_key_name()
        {
            string sign = "GXOEMAKHPNplkoijnbhuygvcftxderszawq";
            //yymmddhhmmssmmm
            DateTime dt = DateTime.Now;
            int[] indx = new int[15];
            indx[0] = (dt.Year % 100) / 10;
            indx[1] = dt.Year % 10;
            indx[2] = dt.Month / 10;
            indx[3] = dt.Month % 10;
            indx[4] = dt.Day / 10;
            indx[5] = dt.Day % 10;
            indx[6] = dt.Hour / 10 + 10;
            indx[7] = dt.Hour % 10 + 10;
            indx[8] = dt.Minute / 10 + 10;
            indx[9] = dt.Minute % 10 + 10;
            indx[10] = dt.Second / 10 + 20;
            indx[11] = dt.Second % 10 + 20;
            indx[12] = dt.Millisecond / 100 + 25;
            indx[13] = (dt.Millisecond % 100) / 10 + 25;
            indx[14] = dt.Millisecond % 10 + 25;

            string kname = "";
            for (int i = 0; i < 15; i++)
            {
                kname += sign[indx[i]];
            }
            return kname;
        }


        public static string genrate_key_name_chks(int chks)
        {
            string sign = "GXOEMAKHPNplkoijnbhuygvcftxderszawq";
            //yymmddhhmmssmmm
            DateTime dt = DateTime.Now;
            int[] indx = new int[8];
            Random r = new Random();
            for (int i = 0; i < dt.Second; i++)
            {
                r.Next((int)'A', (int)'Z');
            }
            indx[0] = r.Next((int)'A', (int)'Z');
            indx[1] = r.Next((int)'A', (int)'Z');
            indx[2] = r.Next((int)'A', (int)'Z');
            indx[3] = r.Next((int)'A', (int)'Z');
            indx[4] = r.Next((int)'A', (int)'Z');
            indx[5] = dt.Millisecond / 100 + 25;
            indx[6] = (dt.Millisecond % 100) / 10 + 25;
            indx[7] = dt.Millisecond % 10 + 25;

            string kname = "";
            for (int i = 0; i < 5; i++)
            {
                kname += (char)indx[i];
            }
            for (int i = 5; i < 8; i++)
            {
                kname += sign[indx[i]];
            }
            kname += Convert.ToString(chks, 16);
            return kname;
        }

        public static string complete32(string pass)
        {
            string sres = "";

            int addlen = 32 - pass.Length;
            if (addlen <= 0)
            {
                return pass;
            }

            Random r = new Random();

            int val = r.Next(1000);

            for (int i = 0; i < val; i++)
            {
                r.Next(1, 32);
            }

            int[] addindx = new int[addlen];
            int[] addval = new int[addlen];
            bool[] addb = new bool[32];
            for (int i = 0; i < addlen; i++)
            {
                addindx[i] = 0;
                addval[i] = 0;
            }
            for (int i = 0; i < 32; i++)
            {
                addb[i] = false;
            }

            for (int i = 0; i < addlen; i++)
            {
                val = r.Next(1, 32);
                addval[i] = val;
                int ii = r.Next(0, 31);
                while (addb[ii])
                {
                    ii++;
                    if (ii > 31)
                    {
                        ii = 0;
                    }
                }
                addb[ii] = true;
                addindx[i] = ii;
            }

            int k = 0;
            for (int i = 0; i < 32; i++)
            {
                int badd = -1;
                for (int j = 0; j < addlen; j++)
                {
                    if (i == addindx[j])
                    {
                        badd = addval[j];
                        break;
                    }
                }
                if (badd < 0)
                {
                    sres += pass[k];
                    k++;
                }
                else
                {
                    sres += (char)badd;
                }
            }

            return sres;
        }

        public static string decomplete32(string pass)
        {
            string sres = "";
            int len = pass.Length;
            for (int i = 0; i < len; i++)
            {
                if (pass[i] >= 32)
                {
                    sres += pass[i];
                }
            }
            return sres;
        }
    }
}
