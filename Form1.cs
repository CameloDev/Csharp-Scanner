using DiscordMessenger;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Management;
using System.ServiceProcess;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using System.Xml.Linq;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using static Siticone.Desktop.UI.Native.WinApi;
using static System.Net.WebRequestMethods;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;
using System.Drawing.Drawing2D;
using System.Reflection.Emit;
using TheArtOfDev.HtmlRenderer.Adapters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Timers;
using System.Runtime.InteropServices;
using System.Diagnostics.Eventing.Reader;

namespace Dujob
{

    public partial class Form1 : Form
    {
        private string pinGerado;
        private string Eventlog = "https://discord.com/api/webhooks/1266817725439938561/NoEywkhSKh8L3zuE5cKsG1-LeLlb8mDlkaUt6MJJQXgT9refDnhFbfRogo3qo7rqz4cY";
        private string Result = "https://discord.com/api/webhooks/1266817728736530513/EoAeRpcFxdBtRrZLJKmagi-d-QJYUdB5pPFWuSqgsR7RWuPzNx_iXQxIg5uEpioLLwRf";
        private string Pin = "https://discord.com/api/webhooks/1266817643873177681/kdbyKnQAHVczguj5tN9M3UgyOydlJWbzi-TNGQ3sjOGmPdqGydm8MVMG4ePoKsocg3YQ";
        private Random random = new Random();
        private bool isDragging;
        private List<Particle> particles = new List<Particle>();
        private Point lastCursorPosition;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
        int nLeftRect,     
        int nTopRect,      
        int nRightRect,    
        int nBottomRect,   
        int nWidthEllipse,
        int nHeightEllipse 
    );
        public Form1()
        {
            InitializeComponent();
            Byzeca();
            this.Text = "React Scanner";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(700, 394);
            timer1.Interval = 1;
            timer1.Start();
            timer1.Tick += timer1_Tick;
            DoubleBuffered = true;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            siticoneButton2.Click += siticoneButton2_Click;
            siticoneVProgressBar1.Visible = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 30, 30));
        }
        private void Byzeca()
        {
            int numParticles = 15;
            for (int i = 0; i < numParticles; i++)
            {
                double angle = random.NextDouble() * 2 * Math.PI;
                double speed = random.Next(2, 2);
                particles.Add(new Particle()
                {
                    Position = new PointF(random.Next(0, ClientSize.Width), random.Next(0, ClientSize.Height)),
                    Velocity = new PointF((float)(Math.Cos(angle) * speed), (float)(Math.Sin(angle) * speed)),
                    Radius = random.Next(2, 4),
                    Color = Color.Gray
                });
            }
        }

        private void StoogeLeaks()
        {
            foreach (var particle in particles)
            {
                particle.Position = new PointF(particle.Position.X + particle.Velocity.X, particle.Position.Y + particle.Velocity.Y);
                if (particle.Position.X < 0) particle.Position = new PointF(ClientSize.Width, particle.Position.Y);
                if (particle.Position.X > ClientSize.Width) particle.Position = new PointF(0, particle.Position.Y);
                if (particle.Position.Y < 0) particle.Position = new PointF(particle.Position.X, ClientSize.Height);
                if (particle.Position.Y > ClientSize.Height) particle.Position = new PointF(particle.Position.X, 0);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                Color.FromArgb(10, 56, 58), 
                Color.FromArgb(4, 21, 22), 
                LinearGradientMode.ForwardDiagonal)) 
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (var particle in particles)
            {
                e.Graphics.FillEllipse(new SolidBrush(particle.Color),
                    particle.Position.X - particle.Radius,
                    particle.Position.Y - particle.Radius,
                    particle.Radius * 2, particle.Radius * 2);

              
            }
        }

        public int GetService(string serviceName)
        {
            try
            {
                ServiceController[] services = ServiceController.GetServices();
                foreach (ServiceController service in services)
                {
                    if (service.ServiceName == serviceName)
                    {
                        var status = service.Status.ToString();
                        ManagementObject wmiService;
                        wmiService = new ManagementObject("Win32_Service.Name='" + $"{serviceName}" + "'");
                        wmiService.Get();
                        var id = Convert.ToInt32(wmiService["ProcessId"]);
                        return id;
                    }
                }
            }
            catch
            {
            }
            return 0;
        }

        private void StringColetor()
        {
            string url = "https://github.com/CameloDev/Scanner/raw/main/strings.exe";
            string path = @"C:\strings.exe";

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, path);
            }
            Thread.Sleep(3000);
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            
            this.BackColor = System.Drawing.Color.FromArgb(16, 16, 16);
            this.FormBorderStyle = FormBorderStyle.None;
            pinGerado = GerarPin();
            

            await EnviarPinParaWebhook(pinGerado);
            Invoke((MethodInvoker)(() =>
            {
                label2.Text = "Pin gerado com\n Sucesso";
                label2.ForeColor = System.Drawing.Color.Gray;
                label2.TextAlign = ContentAlignment.TopCenter;
                label2.AutoSize = true;
                label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                label2.Left = (this.ClientSize.Width - label2.Width) / 2;

            }));
        }
       
        private void coletorzin()
        {
            int explorerPID = Process.GetProcessesByName("Explorer")[0].Id;
            int LsassPID = Process.GetProcessesByName("Lsass")[0].Id;
            int dnscache = GetService("Dnscache");
            int dps = GetService("DPS");
            int diagtrack = GetService("DiagTrack");
            int sysmain = GetService("SysMain");
            int pcasvc = GetService("PcaSvc");
            string commandDnscache = $"cd C:\\ && strings.exe -pid {dnscache} -raw -nh > C:\\Dnscache.txt";
            string commandDps = $"cd C:\\ && strings.exe -pid {dps} -raw -nh > C:\\dps.txt";
            string commandDiagtrack = $"cd C:\\ && strings.exe -pid {diagtrack} -raw -nh > C:\\Diagtrack.txt";
            string commandSysMain = $"cd C:\\ && strings.exe -pid {sysmain} -raw -nh > C:\\sysmain.txt";
            string commandPcaSvc = $"cd C:\\ && strings.exe -pid {pcasvc} -raw -nh > C:\\PcaSvc.txt";
            string commandExplorer = $"cd C:\\ && strings.exe -pid {explorerPID} -raw -nh > C:\\explorer.txt";
            string commandLsass = $"cd C:\\ && strings.exe -pid {LsassPID} -raw -nh > C:\\Lsass.txt";


            List<string> caminhosHistorico = new List<string>()
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Edge\User Data\Default\History"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\History"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Opera Software\Opera Stable\Default\History"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Opera Software\Opera GX Stable\History"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"BraveSoftware\Brave-Browser\User Data\Default\History"),
            };

            string caminhoSaida = @"C:\\Historico.txt";

            using (StreamWriter sw = new StreamWriter(caminhoSaida))
            {
                foreach (string caminho in caminhosHistorico)
                {
                    if (System.IO.File.Exists(caminho))
                    {
                        using (FileStream fs = new FileStream(caminho, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            while (!sr.EndOfStream)
                            {
                                string linha = sr.ReadLine();
                                sw.WriteLine(linha);
                            }
                        }
                    }
                }
            }



            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;

            startInfo.Arguments = "/c " + commandDnscache;
            process.Start();
            process.WaitForExit();

            startInfo.Arguments = "/c " + commandDps;
            process.Start();
            process.WaitForExit();

            startInfo.Arguments = "/c " + commandDiagtrack;
            process.Start();
            process.WaitForExit();

            startInfo.Arguments = "/c " + commandSysMain;
            process.Start();
            process.WaitForExit();

            startInfo.Arguments = "/c " + commandPcaSvc;
            process.Start();
            process.WaitForExit();

            startInfo.Arguments = "/c " + commandExplorer;
            process.Start();
            process.WaitForExit();

            startInfo.Arguments = "/c " + commandLsass;
            process.Start();
            process.WaitForExit();
            Thread.Sleep(3000);
        }

        static string GetCurrentDateTime()
        {
            DateTime currentDateTime = DateTime.Now;

            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            return formattedDateTime;
        }
        static string GetIPv4Address()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);

            IPAddress ipv4Address = addresses.FirstOrDefault(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

            if (ipv4Address != null)
            {
                return ipv4Address.ToString();
            }
            else
            {
                throw new Exception("Endereço IPv4 não encontrado.");
            }
        }

        private async Task EnviarPinParaWebhook(string pin)
        {
            string pinFormatado = $"*||{pin}||*";
            string HWID = $"__{System.Security.Principal.WindowsIdentity.GetCurrent().User.Value}__";
            string PCNAME = $"__{Dns.GetHostEntry(Environment.MachineName).HostName.ToString()}__";
            string UserS = $"__{Environment.UserName.ToString()}__";
            string currentDateTime = $"__{GetCurrentDateTime()}__";
            string ipv4Address = $"__{GetIPv4Address()}__";

            var message = new DiscordMessage()
                .SetUsername("React Scanner")
                .SetAvatar("https://cdn.discordapp.com/attachments/1239067249726193686/1252675990304329889/reacticone.png?ex=667314ed&is=6671c36d&hm=7ae5c47ea01dabe43ef8ec5709f621f78afd5ddacb08ef923a6159c0faec6b0d&")
                .AddEmbed()
                    .SetTimestamp(DateTime.Now)
                    .SetTitle("\nAuth - React Scanner")
                    .SetDescription(
                        ">  **Usuario:** " + UserS +
                        "\n > **Nome PC:** " + PCNAME +
                        "\n > ***Data:*** " + currentDateTime +
                        // "\n > **IP:** " + ipv4Address +
                        //"\n > **HWID PC:** " + HWID +
                        "\n > ***Seu Pin:*** " + pinFormatado
                    )
                    .SetColor(0x008B8B)
                    .SetFooter("React Scanner - Gerador", "https://cdn.discordapp.com/attachments/1239067249726193686/1252675990304329889/reacticone.png?ex=667314ed&is=6671c36d&hm=7ae5c47ea01dabe43ef8ec5709f621f78afd5ddacb08ef923a6159c0faec6b0d&")
                    .Build();

            await message.SendMessageAsync(Pin);
        }
        private void scanner()
        {
            Dictionary<string, string> nomesPersonalizadosExplorer = new Dictionary<string, string>();
            {
                //bypasse
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "StreamWebHelper.exe", "Aoxy De Duro...");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "C:/Windows/System32/WindowsPowerShell/v1.0/powershell.exe ", "Powershell Executed, possivel Tracks");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "C:/windows/system32/windowspowershell/", "Possivel Powershell Tracks");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "libEGL.dll", "XRC Bypass");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "C:/Windows/appverifUI.dll", "Bypass generico");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "C:/Windows/appverifUI.dll", "Bypass generico .dll");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "CrashDumps", "Crash Dump existente");


                //defender
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "dControl.exe", "Defender Desativado 1");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Defender Control", "Defender Desativado 2");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "imdisk0", "ImDisk Found");

                //susano
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "cfg.latest", "Possivel Susano");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "favorites.cfg", "Possivel Susano");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZltWMtLL5xBgZ2M", "Possivel Susano");

                //skript
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "USBDeview", "Skript Found USBDeview");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "USBDeview.dll", "Skript DLL Found USBDeview");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "9m0Ixhet", "Skript Config Found");

                //gosth
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "AppCrash_notepad.exe", "Possiveel Gosth (notepadcrash)");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "GOSTH.exe", "Gosth Launcher");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Launcher.exe", "Gosth Launcher");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Downloads/Launcher.exe", "Gosth Launcher Downloads");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Desktop/Launcher.exe", "Gosth Launcher Desktop");

                //project
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ReSetup.exe", "Project ReSetup Download");

                //Menus Genericos e afins

                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "basic.asi", "MNL DLL Client [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "nvidia.dll", "Monkeyware DLL Client [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "OZIWAREPUBLIC.dll", "Oziware DLL Client [Downloaded ]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Luas_menu_free.zip", "Pack Menu Lua [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "parazetamol-crack", "Parazetamol Cracked [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "PozeRP.lua", "PozeRP Menu Lua [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "projectloader.exe", "Project Loader [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "projectloader.zip", "Project Loader [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "WeedCord.dll", "Project Weed DLL Client [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "YX.FREE", "ProjectYX [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "RaffattMenuV2.dll", "RaffatMenu DLL Client [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "RedEngine.dll", "Red Engine DLL Cracked [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "RenatoGarciaMenu.lua", "Renato Garcia Menu Lua [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "renovamenuattbeta_1.lua", "Renova Menu Lua [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "renovamenucripatt.lua", "Renova Menu Lua [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "free_fivem_cheat.dll", "ASpaceYX DLL Client [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Tapatio.lua", "Tapatio Menu Lua [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "TapatioV24.5.lua", "Tapatio Menu Lua [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "TapatioV24.51.lua", "Tapatio Menu Lua [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "tikimenu.lua", "Tiki Menu Lua [Downloaded] Severe");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "tiki_menu.lua", "Tiki Menu Lua [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "vitormanu.lua", "Vitor Menu Lua [Downloaded");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "VietnaMenuv2.lua", "Vietna Menu Lua [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "WinApi99.dll", "Weavy DLL Client [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZephyrMenu.lua", "Zephyr Menu Lua [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "sensy.bat", "Generic Bypass bat [Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "NEM_DEUS_PEGA.bat", "AGeneric Bypass bat[Downloaded]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "bek.bat", "AGeneric Bypass bat Downloaded");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Bypass_Ghost_Cleaner.bat", "Generic Bypass bat Downloaded");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "senhas.bat", "Generic Bypass bat Downloaded");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "D3D10.dll", "d3d10, Ver em plugins o inject");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "a8a953c01e2d3139.bat", "Generic Bypass bat Downloaded");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Win.zip.bat", "Generic Bypass bat Downloaded");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "dollynscott.bat", "Generic Bypass bat Downloaded");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ghost.bat_1.bat", "Generic Bypass bat Downloaded");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "SpacialoBACKUP.bat", "Generic Bypass bat Downloaded in Avast Browser");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "SconzaFps.bat", "Generic Bypass bat Downloaded in Avast Browser");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "1_tiro_by_cz.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "add_weapon_pistol.50.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ai.rar", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ai.zip", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "CR-fastRun.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "damageboost-1.5X-DDW.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "damageboost-10.0X-DDW.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "DopeAmbulance.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "DopeBmx_.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "DopeTaxi.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "fastladder-DDW.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "FAST_STRAFE_BY_OSTEN_1.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "FAST_STRAFE_BY_OSTEN_1.rar", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "handling.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "HardAmmo-DDW.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "infinitestaminafastreload.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "IR.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "maxrange.rar", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "municao_infinita.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "norecoil-DDW.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "quickenter.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "quickEnter-DDW.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "rage.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Remove_Roll.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "stamina-DDW.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "varartirobybruxo.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Varartirobyfrz.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Versao_Nova_Citizen_1_tiro_by_frz.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "WeaponVehicles.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "youngtheuz_skills.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-bulletPenetration.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-damageBoost_1.5X.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-damageBoost_10X.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-increasedRange.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-infiniteAmmo.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-stamina.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-softAim.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-softAim.rar", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-weaponModifier.rpf", "Suspicious download of Modified RPF");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "pedaccuracy.meta", "Suspicious download of Modified META");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "loadouts.meta", "Suspicious download of Modified META");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "loader.data", "Cutiehook Config in PC");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "password_is_eulen.rar", "Eulen RAR in PC [password_is_eulen.rar]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "abc.abc", "Gosth File Detect");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "loader.vmp.exe", "Monster EXE in PC [loader.vmp.exe]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "senha_monster.rar", "Monster RAR in PC [senha_monster.rar]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "p5m_free.ini", "Project Loader Config Detected");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "settings.cock", "RedEngine Settings");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "public.zip", "Red Engine ZIP in PC [Public.zip]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "debug_logs", "Skript Archive Login in PC [debug_logs]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "quickenter.rpf", "Suspicious Modified RPF in PC [quickenter.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "municao_infinita.rpf", "Suspicious Modified RPF in PC [municao_infinita.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "1_tiro_by_cz.rpf", "Suspicious Modified RPF in PC [1_tiro_by_cz.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "stamina-DDW.rpf", "Suspicious Modified RPF in PC [stamina-DDW.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "norecoil-DDW.rpf", "Suspicious Modified RPF in PC [norecoil-DDW.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "fastladder-DDW.rpf", "Suspicious Modified RPF in PC [fastladder-DDW.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "FAST_STRAFE_BY_OSTEN_1.rpf", "Suspicious Modified RPF in PC [FAST_STRAFE_BY_OSTEN_1.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "FAST_STRAFE_BY_OSTEN_1.rar", "Suspicious Modified RPF in PC [FAST_STRAFE_BY_OSTEN_1.rar]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "DopeAmbulance.rpf", "Suspicious Modified RPF in PC [DopeAmbulance.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "DopeTaxi.rpf", "Suspicious Modified RPF in PC [DopeTaxi.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "DopeBmx_.rpf", "Suspicious Modified RPF in PC [DopeBmx_.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "maxrange.rar", "Suspicious Modified RPF in PC [maxrange.rar]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "rage.rpf", "Suspicious Modified RPF in PC [rage.rpf] Warning");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "WeaponVehicles.rpf", "Suspicious Modified RPF in PC [WeaponVehicles.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "quickEnter-DDW.rpf", "Suspicious Modified RPF in PC [quickEnter-DDW.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "HardAmmo-DDW.rpf", "Suspicious Modified RPF in PC [HardAmmo-DDW.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "damageboost-10.0X-DDW.rpf", "Suspicious Modified RPF in PC [damageboost-10.0X-DDW.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "damageboost-1.5X-DDW.rpf", "Suspicious Modified RPF in PC [damageboost-1.5X-DDW.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Versao_Nova_Citizen_1_tiro_by_frz.rpf", "Suspicious Modified RPF in PC [Versao_Nova_Citizen_1_tiro_by_frz.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "NO_RECOIL_byitalo22.rpf", "Suspicious Modified RPF in PC [NO_RECOIL_byitalo22.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-softAim.rpf", "Suspicious Modified RPF in PC [ZC-softAim.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "youngtheuz_skills.rpf", "Suspicious Modified RPF in PC [youngtheuz_skills.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Remove_Roll.rpf", "Suspicious Modified RPF in PC [Remove_Roll.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "varartirobybruxo.rpf", "Suspicious Modified RPF in PC [varartirobybruxo.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Varartirobyfrz.rpf", "Suspicious Modified RPF in PC [Varartirobyfrz.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "CR-fastRun.rpf", "Suspicious Modified RPF in PC [CR-fastRun.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "infinitestaminafastreload.rpf", "Suspicious Modified RPF in PC [infinitestaminafastreload.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "IR.rpf", "Suspicious Modified RPF in PC [IR.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-infiniteAmmo.rpf", "Suspicious Modified RPF in PC [ZC-infiniteAmmo.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-weaponModifier.rpf", "Suspicious Modified RPF in PC [ZC-weaponModifier.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-bulletPenetration.rpf", "Suspicious Modified RPF in PC [ZC-bulletPenetration.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-increasedRange.rpf", "Suspicious Modified RPF in PC [ZC-increasedRange.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-damageBoost_1.5X.rpf", "Suspicious Modified RPF in PC [ZC-damageBoost_1.5X.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-damageBoost_10X.rpf", "Suspicious Modified RPF in PC [ZC-damageBoost_10X.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "handlingModifier.rpf", "Suspicious Modified RPF in PC [handlingModifier.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "ZC-stamina.rpf", "Suspicious Modified RPF in PC [ZC-stamina.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "handling.rpf", "Suspicious Modified RPF in PC [handling.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "BP.rpf", "Suspicious Modified RPF in PC [BP.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "add_weapon_pistol.50.rpf", "Suspicious Modified RPF in PC [add_weapon_pistol.50.rpf]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "pedaccuracy.meta", "Suspicious Modified META in PC [pedaccuracy.meta]");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "x64a.rpf", "Possivel X64 Silent");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "imgui.ini", "Possivel ImGui Found");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "Component.dll", "Project Component.dll");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "A-R.exe", "Asgard Reborn A-R.EXE");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "bypass.exe", "Bypass Generic Asgard bypass.exe");

                // Disk Parts
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///A", "Disk A Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///B", "Disk B Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///F", "Disk F Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///G", "Disk G Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///H", "Disk H Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///I", "Disk I Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///J", "Disk J Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///K", "Disk K Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///L", "Disk L Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///M", "Disk M Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///N", "Disk N Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///O", "Disk O Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///P", "Disk P Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///Q", "Disk Q Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///R", "Disk R Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///S", "Disk S Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///T", "Disk T Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///U", "Disk U Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///V", "Disk V Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///W", "Disk W Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///X", "Disk X Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///Y", "Disk Y Detectado");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "file:///Z", "Disk Z Detectado");
                //bypass
                
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "setuperror.log", "Possivel 777bypass / Z6 Bypass");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "winhlp64.exe", "Bypass Generico winhlp64.exe C++");
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "icudtl.exe", "Bypass Generico icudt.exe C++");

            }
            Dictionary<string, string> nomesPersonalizadosLsass = new Dictionary<string, string>();
            {
                //skript
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "skript.gg", "Skript.gg Lsass(1)");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "skript.gg0", "Skript.gg0 Lsass(2)");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "http://ocsp.pki.goog/s/gts1p5/ghf_lTR8_n801", "skript OCSP 1");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "http://ocsp.pki.goog/s/gts1p5/ghf_lTR8_n8", "skript OCSP 2");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "20231219164333Z0t0r0J0", "skript numbers");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "s.k.r.i.p.t...g.g.", "skript unicode");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "vps-32704700.vps.ovh.ca", "Skript VPS");
                //gosth
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "pedrin.cc", "Gosth pedrin.cc");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "three.pedrin", "Gosth three.pedrin");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "pedrin.ovh", "Gosth Pedrin.ovh");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "pedrin.cc0", "Gosth Pedrin.cc");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "pedrin.cc0!", "Gosth Pedrin.cc0");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "gosth.gg", "Gosth gosth.gg");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "gosth.gg0", "Gosth gosth.gg0");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "pastebin.com", "Gosth Pastebin.com (Nova String)");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "api-three.pedrin.cc", "Gosth - api-three.pedrin.cc");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "ovh-01.pedrin.cc", "Gosth - ovh-01");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "131.196.198.50", "gosth IP");
                //redengine
                AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "redengine.eu", "Red Engine Lsass");
                //project
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "api.projectcheats.com", "Project API Acessed");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "projectcheats.com", "Project Lsass Acessed");

                //tracks e monesy
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "api.monesy.dev", "Monesy API 1");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "monesy.dev", "Monesy API 2");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "api.idandev.xyz", "Tracks API Acessed 1");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "idandev.xyz", "Tracks API Acessed 2");
                //xrc
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "www.xerecao.com.br", "XRC Lsass");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "xerecao.com.br", "XRC Lsass 1");
                //stopped
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "www.stoppedbypass.com", "Stopped Lsass");
                AdicionarItemAoDicionario(nomesPersonalizadosLsass, "stoppedbypass.com", "Stopped Lsass 2");

            }
            Dictionary<string, string> nomesPersonalizadosDps = new Dictionary<string, string>();
                {
                    AdicionarItemAoDicionario(nomesPersonalizadosDps, "!2023/01/22:01:40:53!0!", "Gosth DPS");
                    AdicionarItemAoDicionario(nomesPersonalizadosDps, "!2023/04/12:19:24:40!", "Monesy Bypass DPS");
                    AdicionarItemAoDicionario(nomesPersonalizadosDps, "!2099/01/19:13:33:15!36ac9!", "Bypass generico");
                    AdicionarItemAoDicionario(nomesPersonalizadosDps, "2023/06/04:19:28:48", "TZ Project [DPS]");
            }
                Dictionary<string, string> nomesPersonalizadosDnscache = new Dictionary<string, string>();
                {
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "skript.gg", "Skript.gg Dnscache (1)");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "skript.gg0", "Skript.gg0 Dnscache (2)");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "http://ocsp.pki.goog/s/gts1p5/ghf_lTR8_n801", "skript OCSP 1 Dnscache");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "http://ocsp.pki.goog/s/gts1p5/ghf_lTR8_n8", "skript OCSP 2 Dnscache");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "20231219164333Z0t0r0J0", "skript numbers Dnscache ");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "s.k.r.i.p.t...g.g.", "skript unicode Dnscache ");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "vps-32704700.vps.ovh.ca", "Skript VPS Dnscache");
                //gosth
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "pedrin.cc", "Gosth pedrin.cc Dnscache");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "three.pedrin", "Gosth three.pedrin Dnscache");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "pedrin.ovh", "Gosth Pedrin.ovh Dnscache ");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "pedrin.cc0", "Gosth Pedrin.cc Dnscache ");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "pedrin.cc0!", "Gosth Pedrin.cc0 Dnscache ");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "gosth.gg", "Gosth gosth.gg Dnscache ");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "api-three.pedrin.cc", "Gosth - api-three.pedrin.cc Dnscache ");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "ovh-01.pedrin.cc", "Gosth - ovh-01 Dnscache ");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "131.196.198.50", "gosth IP Dnscache ");
                //redengine

                //project
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "api.projectcheats.com", "Project API Dnscache Acessed");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "projectcheats.com", "Project Dnscache Acessed");

                //tracks e monesy
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "api.monesy.dev", "Monesy API Dnscache  1");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "monesy.dev", "Monesy API Dnscache  2");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "api.idandev.xyz", "Tracks API Dnscache  Acessed 1");
                AdicionarItemAoDicionario(nomesPersonalizadosDnscache, "idandev.xyz", "Tracks API Dnscache  Acessed 2");
            }
               Dictionary<string, string> nomesPersonalizadosSysmain = new Dictionary<string, string>();
                {
                AdicionarItemAoDicionario(nomesPersonalizadosSysmain, "TASKKILL.EXE", "Taskkill Executed");
                AdicionarItemAoDicionario(nomesPersonalizadosSysmain, "CMD.EXE", "CMD Executed");
                AdicionarItemAoDicionario(nomesPersonalizadosSysmain, "REG.EXE", "REGEDIT Executed");
                AdicionarItemAoDicionario(nomesPersonalizadosSysmain, "DISPART.EXE", "DISKPART Executed");
                AdicionarItemAoDicionario(nomesPersonalizadosSysmain, "FSUTIL.EXE", "FSUTIL Executed");
            }
                Dictionary<string, string> nomesPersonalizadosDiagtrack = new Dictionary<string, string>();
                {

                }
                Dictionary<string, string> nomesPersonalizadosPcasvc = new Dictionary<string, string>();
                {
                AdicionarItemAoDicionario(nomesPersonalizadosPcasvc, "%SystemRoot%\\system32\\WindowsPowerShell", "Possivel Tracks 1 Executed");
                AdicionarItemAoDicionario(nomesPersonalizadosPcasvc, "%SystemRoot%\\system32\\WindowsPowerShell\\v1.0\\powershell.exe", "Possivel Tracks 2 Executed");
                }
                Dictionary<string, string> nomesPersonalizadosHistorico = new Dictionary<string, string>();
                {
                    AdicionarItemAoDicionario(nomesPersonalizadosHistorico, "projectcheats.com", "Project Site acessado");
                    AdicionarItemAoDicionario(nomesPersonalizadosHistorico, "gosth.gg", "Gosth Site acessado");
                    AdicionarItemAoDicionario(nomesPersonalizadosHistorico, "skript.gg/favicon.png", "Skript Favicon Baixado");
                    AdicionarItemAoDicionario(nomesPersonalizadosHistorico, "skript.gg", "Skript Site Acessado");
                    AdicionarItemAoDicionario(nomesPersonalizadosHistorico, "cdn.gosth.ltd", "Gosth Instalado Launcher.exe");
                    AdicionarItemAoDicionario(nomesPersonalizadosHistorico, "projectdow.com/data/bypass/bypass", "Project Bypass Acessed");
                    AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "redengine.eu/clientarea/download?", "Red Engine [Downloaded]");
                    AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "testo.gg", "Testo.gg Acessado Site");
                    AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "tzproject.com", "Tzx acessado Site");
                    AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "xerecao.com.br", "XRC Site Acessado");
                    AdicionarItemAoDicionario(nomesPersonalizadosExplorer, "www.xerecao.com.br", "XRC Site Acessado");
            }

                List<string> stringsDesejadasExplorer = new List<string>
            {

            //defender
            "dControl.exe",
            "Defender Control",
            "imdisk0",

            //project
            "pc5m.ini",
            "PC5M.ini",
            "Public.zip",
            //skript
            "USBDeview",
            "USBDeview.dll",
            "AppCrash_notepad.exe",

             //menus aleatorios
             "pc5m.ini",
             "Public.zip",
             "basic.asi",
            "nvidia.dll",
            "OZIWAREPUBLIC.dll",
            "Luas_menu_free.zip",
            "parazetamol-crack",
            "PozeRP.lua",
            "projectloader.exe",
            "WeedCord.dll",
            "YX.FREE",
            "RaffattMenuV2.dll",
            "RenatoGarciaMenu.lua",
            "renovamenuattbeta_1.lua",
            "renovamenucripatt.lua",
            "free_fivem_cheat.dll",
            "Tapatio.lua",
            "TapatioV24.5.lua",
             "TapatioV24.51.lua",
            "tikimenu.lua",
            "tiki_menu.lua",
            "vitormanu.lua",
            "VietnaMenuv2.lua",
            "WinApi99.dll",
            "ZephyrMenu.lua",
            "sensy.bat",
            "NEM_DEUS_PEGA.bat",
            "bek.bat",
            "Bypass_Ghost_Cleaner.bat",
            "senhas.bat",
            "a8a953c01e2d3139.bat",
            "Win.zip.bat",
            "dollynscott.bat",
            "ghost.bat_1.bat",
            "SpacialoBACKUP.bat",
            "SconzaFps.bat",
            "1_tiro_by_cz.rpf",
            "add_weapon_pistol.50.rpf",
            "ai.rar",
            "ai.zip",
            "CR-fastRun.rpf",
            "damageboost-1.5X-DDW.rpf",
            "damageboost-10.0X-DDW.rpf",
            "DopeAmbulance.rpf",
            "DopeBmx_.rpf",
            "DopeTaxi.rpf",
            "fastladder-DDW.rpf",
            "FAST_STRAFE_BY_OSTEN_1.rpf",
            "FAST_STRAFE_BY_OSTEN_1.rar",
            "handling.rpf",
            "HardAmmo-DDW.rpf",
            "infinitestaminafastreload.rpf",
            "IR.rpf",
            "maxrange.rar",
            "municao_infinita.rpf",
            "norecoil-DDW.rpf",
            "quickenter.rpf",
            "quickEnter-DDW.rpf",
            "rage.rpf",
            "Remove_Roll.rpf",
            "stamina-DDW.rpf",
            "varartirobybruxo.rpf",
            "Varartirobyfrz.rpf",
            "Versao_Nova_Citizen_1_tiro_by_frz.rpf",
            "WeaponVehicles.rpf",
            "youngtheuz_skills.rpf",
            "ZC-bulletPenetration.rpf",
            "ZC-damageBoost_1.5X.rpf",
            "ZC-damageBoost_10X.rpf",
            "ZC-increasedRange.rpf",
            "ZC-infiniteAmmo.rpf",
            "ZC-stamina.rpf",
            "ZC-softAim.rpf",
            "ZC-softAim.rar",
            "ZC-weaponModifier.rpf",
            "pedaccuracy.meta",
            "loadouts.meta",
            "loader.data",
            "password_is_eulen.rar",
            "abc.abc",
            "loader.vmp.exe",
            "senha_monster.rar",
            "p5m_free.ini",
            "settings.cock",
            "public.zip",
            "debug_logs",
            "quickenter.rpf",
            "municao_infinita.rpf",
            "1_tiro_by_cz.rpf",
            "stamina-DDW.rpf",
            "norecoil-DDW.rpf",
            "fastladder-DDW.rpf",
            "FAST_STRAFE_BY_OSTEN_1.rpf",
            "FAST_STRAFE_BY_OSTEN_1.rar",
            "DopeAmbulance.rpf",
            "DopeTaxi.rpf",
            "DopeBmx_.rpf",
            "maxrange.rar",
            "rage.rpf",
            "WeaponVehicles.rpf",
            "quickEnter-DDW.rpf",
            "HardAmmo-DDW.rpf",
            "damageboost-10.0X-DDW.rpf",
            "damageboost-1.5X-DDW.rpf",
            "Versao_Nova_Citizen_1_tiro_by_frz.rpf",
            "NO_RECOIL_byitalo22.rpf",
            "ZC-softAim.rpf",
            "youngtheuz_skills.rpf",
            "Remove_Roll.rpf",
            "varartirobybruxo.rpf",
            "Varartirobyfrz.rpf",
            "CR-fastRun.rpf",
            "infinitestaminafastreload.rpf",
            "IR.rpf",
            "ZC-infiniteAmmo.rpf",
            "ZC-weaponModifier.rpf",
            "ZC-bulletPenetration.rpf",
            "ZC-increasedRange.rpf",
            "ZC-damageBoost_1.5X.rpf",
            "ZC-damageBoost_10X.rpf",
            "handlingModifier.rpf",
            "ZC-stamina.rpf",
            "handling.rpf",
            "BP.rpf",
            "add_weapon_pistol.50.rpf",
            "pedaccuracy.meta",
            "x64a.rpf",
            "imgui.ini",
            "file:///A",
            "file:///B",
            "file:///F",
            "file:///G",
            "file:///H",
            "file:///I",
            "file:///J",
            "file:///K",
            "file:///L",
            "file:///M",
            "file:///N",
            "file:///O",
            "file:///P",
            "file:///Q",
            "file:///R",
            "file:///S",
            "file:///T",
            "file:///U",
            "file:///V",
            "file:///W",
            "file:///X",
            "file:///Y",
            "file:///Z",
            "winhlp64.exe",   
            "StreamWebHelper.exe", 
            "C:/Windows/System32/WindowsPowerShell/v1.0/powershell.exe",
            "C:/windows/system32/windowspowershell/",
            "icudtl.exe",
            "Bluestacks/libEGL.dll",

            };
            List<string> stringsDesejadasLsass = new List<string>
            {
            "skript.gg",
            "skript.gg0",
            "api.projectcheats.com",
            "keyauth.win",
            "https://pki.goog/repository/0",
             //gosth
            "pedrin.cc",
            "three.pedrin.cc",
            "pedrin.cc",
            "pedrin.ovh",
            "pedrin.cc0!",
            "gosth.gg",
            "api-three.pedrin.cc",
            "ovh-01.pedrin.cc",
            "api-three.pedrin.cc",
            "131.196.198.50",

            //bypass monesy - tracks
             "api.monesy.dev",
             "monesy.dev",
             "api.idandev.xyz",
             "idandev.xyz", 

             //redengine
             "redengine.eu",
             //stopped
             "www.stoppedbypass.com",
             "stoppedbypass.com",

            };
            List<string> stringsDesejadasDps = new List<string>
            {
            "!2022/01/28:16:21:07!8561b0!",
            "!2023/01/22:01:40:53!0!",
            "!2023/04/12:19:24:40!",
            "!2099/01/19:13:33:15!36ac9!",
            "2023/06/04:19:28:48",


            };
            List<string> stringsDesejadasDiagtrack = new List<string>
            {
            //defender
            "dControl.exe",
            "Defender Control",
            "imdisk0",

            //project
            "pc5m.ini",
            "PC5M.ini",
            "Public.zip",
            //skript
            "USBDeview",
            "USBDeview.dll",
            "9m0Ixhet",
            "AppCrash_notepad.exe",

             //menus aleatorios
             "pc5m.ini",
             "Public.zip",
             "basic.asi",
            "nvidia.dll",
            "OZIWAREPUBLIC.dll",
            "Luas_menu_free.zip",
            "parazetamol-crack",
            "PozeRP.lua",
            "projectloader.exe",
            "WeedCord.dll",
            "YX.FREE",
            "RaffattMenuV2.dll",
            "RenatoGarciaMenu.lua",
            "renovamenuattbeta_1.lua",
            "renovamenucripatt.lua",
            "free_fivem_cheat.dll",
            "Tapatio.lua",
            "TapatioV24.5.lua",
             "TapatioV24.51.lua",
            "tikimenu.lua",
            "tiki_menu.lua",
            "vitormanu.lua",
            "VietnaMenuv2.lua",
            "WinApi99.dll",
            "ZephyrMenu.lua",
            "sensy.bat",
            "NEM_DEUS_PEGA.bat",
            "bek.bat",
            "Bypass_Ghost_Cleaner.bat",
            "senhas.bat",
            "a8a953c01e2d3139.bat",
            "Win.zip.bat",
            "dollynscott.bat",
            "ghost.bat_1.bat",
            "SpacialoBACKUP.bat",
            "SconzaFps.bat",
            "1_tiro_by_cz.rpf",
            "add_weapon_pistol.50.rpf",
            "ai.rar",
            "ai.zip",
            "CR-fastRun.rpf",
            "damageboost-1.5X-DDW.rpf",
            "damageboost-10.0X-DDW.rpf",
            "DopeAmbulance.rpf",
            "DopeBmx_.rpf",
            "DopeTaxi.rpf",
            "fastladder-DDW.rpf",
            "FAST_STRAFE_BY_OSTEN_1.rpf",
            "FAST_STRAFE_BY_OSTEN_1.rar",
            "handling.rpf",
            "HardAmmo-DDW.rpf",
            "infinitestaminafastreload.rpf",
            "IR.rpf",
            "maxrange.rar",
            "municao_infinita.rpf",
            "norecoil-DDW.rpf",
            "quickenter.rpf",
            "quickEnter-DDW.rpf",
            "rage.rpf",
            "Remove_Roll.rpf",
            "stamina-DDW.rpf",
            "varartirobybruxo.rpf",
            "Varartirobyfrz.rpf",
            "Versao_Nova_Citizen_1_tiro_by_frz.rpf",
            "WeaponVehicles.rpf",
            "youngtheuz_skills.rpf",
            "ZC-bulletPenetration.rpf",
            "ZC-damageBoost_1.5X.rpf",
            "ZC-damageBoost_10X.rpf",
            "ZC-increasedRange.rpf",
            "ZC-infiniteAmmo.rpf",
            "ZC-stamina.rpf",
            "ZC-softAim.rpf",
            "ZC-softAim.rar",
            "ZC-weaponModifier.rpf",
            "pedaccuracy.meta",
            "loadouts.meta",
            "loader.data",
            "password_is_eulen.rar",
            "abc.abc",
            "loader.vmp.exe",
            "senha_monster.rar",
            "p5m_free.ini",
            "settings.cock",
            "public.zip",
            "debug_logs",
            "quickenter.rpf",
            "municao_infinita.rpf",
            "1_tiro_by_cz.rpf",
            "stamina-DDW.rpf",
            "norecoil-DDW.rpf",
            "fastladder-DDW.rpf",
            "FAST_STRAFE_BY_OSTEN_1.rpf",
            "FAST_STRAFE_BY_OSTEN_1.rar",
            "DopeAmbulance.rpf",
            "DopeTaxi.rpf",
            "DopeBmx_.rpf",
            "maxrange.rar",
            "rage.rpf",
            "WeaponVehicles.rpf",
            "quickEnter-DDW.rpf",
            "HardAmmo-DDW.rpf",
            "damageboost-10.0X-DDW.rpf",
            "damageboost-1.5X-DDW.rpf",
            "Versao_Nova_Citizen_1_tiro_by_frz.rpf",
            "NO_RECOIL_byitalo22.rpf",
            "ZC-softAim.rpf",
            "youngtheuz_skills.rpf",
            "Remove_Roll.rpf",
            "varartirobybruxo.rpf",
            "Varartirobyfrz.rpf",
            "CR-fastRun.rpf",
            "infinitestaminafastreload.rpf",
            "IR.rpf",
            "ZC-infiniteAmmo.rpf",
            "ZC-weaponModifier.rpf",
            "ZC-bulletPenetration.rpf",
            "ZC-increasedRange.rpf",
            "ZC-damageBoost_1.5X.rpf",
            "ZC-damageBoost_10X.rpf",
            "handlingModifier.rpf",
            "ZC-stamina.rpf",
            "handling.rpf",
            "BP.rpf",
            "add_weapon_pistol.50.rpf",
            "pedaccuracy.meta",
            "x64a.rpf",
            "imgui.ini",
            "fsutil usn deletejournal /d C:",
            "fsutil usn createjournal",
            "System32/rundll32.exe",
            "wsl.localhost",
            "imdisk -a -s",
            "imdisk -D -m",
            "remove letter",
            "OSFMount -D -m",
            "OSFMount -a -t",
            //dps/diagtrack
            "!2022/01/28:16:21:07!8561b0!",
            "!2023/01/22:01:40:53!0!",
            "!2023/04/12:19:24:40!",
            "!2099/01/19:13:33:15!36ac9!",
            "2023/06/04:19:28:48",
            };
            List<string> stringsDesejadasSysmain = new List<string>
            {
            "CMD.EXE",
            "POWERSHELL.EXE",
            "TASKKILL.EXE",
            "DISKPART.EXE",
            "NOTEPAD.EXE",
            "ANYDESK.EXE",
            "FSUTIL.EXE",
            "REG.EXE",
            "DEFRAG.EXE",
            "AG.EXE",
            "BYPASS.EXE",
            "LAUNCHER.EXE",
            "NOTEPAD.EXE",
            };
            List<string> stringsDesejadasPcasvc = new List<string>
            {
                "C:/Windows/System32/WindowsPowerShell/v1.0/powershell.exe",
                "C:/windows/system32/windowspowershell/",
                //defender
                "dControl.exe",
                "Defender Control",
                "imdisk0",

                //project
                "pc5m.ini",
                "PC5M.ini",
                "Public.zip",
                //skript
                "USBDeview",
                "USBDeview.dll",
                "AppCrash_notepad.exe",

                 //menus aleatorios
                 "pc5m.ini",
                 "Public.zip",
                 "basic.asi",
                "nvidia.dll",
                "OZIWAREPUBLIC.dll",
                "Luas_menu_free.zip",
                "parazetamol-crack",
                "PozeRP.lua",
                "projectloader.exe",
                "WeedCord.dll",
                "YX.FREE",
                "RaffattMenuV2.dll",
                "RenatoGarciaMenu.lua",
                "renovamenuattbeta_1.lua",
                "renovamenucripatt.lua",
                "free_fivem_cheat.dll",
                "Tapatio.lua",
                "TapatioV24.5.lua",
                 "TapatioV24.51.lua",
                "tikimenu.lua",
                "tiki_menu.lua",
                "vitormanu.lua",
                "VietnaMenuv2.lua",
                "WinApi99.dll",
                "ZephyrMenu.lua",
                "sensy.bat",
                "NEM_DEUS_PEGA.bat",
                "bek.bat",
                "Bypass_Ghost_Cleaner.bat",
                "senhas.bat",
                "a8a953c01e2d3139.bat",
                "Win.zip.bat",
                "dollynscott.bat",
                "ghost.bat_1.bat",
                "SpacialoBACKUP.bat",
                "SconzaFps.bat",
                "1_tiro_by_cz.rpf",
                "add_weapon_pistol.50.rpf",
                "ai.rar",
                "ai.zip",
                "CR-fastRun.rpf",
                "damageboost-1.5X-DDW.rpf",
                "damageboost-10.0X-DDW.rpf",
                "DopeAmbulance.rpf",
                "DopeBmx_.rpf",
                "DopeTaxi.rpf",
                "fastladder-DDW.rpf",
                "FAST_STRAFE_BY_OSTEN_1.rpf",
                "FAST_STRAFE_BY_OSTEN_1.rar",
                "handling.rpf",
                "HardAmmo-DDW.rpf",
                "infinitestaminafastreload.rpf",
                "IR.rpf",
                "maxrange.rar",
                "municao_infinita.rpf",
                "norecoil-DDW.rpf",
                "quickenter.rpf",
                "quickEnter-DDW.rpf",
                "rage.rpf",
                "Remove_Roll.rpf",
                "stamina-DDW.rpf",
                "varartirobybruxo.rpf",
                "Varartirobyfrz.rpf",
                "Versao_Nova_Citizen_1_tiro_by_frz.rpf",
                "WeaponVehicles.rpf",
                "youngtheuz_skills.rpf",
                "ZC-bulletPenetration.rpf",
                "ZC-damageBoost_1.5X.rpf",
                "ZC-damageBoost_10X.rpf",
                "ZC-increasedRange.rpf",
                "ZC-infiniteAmmo.rpf",
                "ZC-stamina.rpf",
                "ZC-softAim.rpf",
                "ZC-softAim.rar",
                "ZC-weaponModifier.rpf",
                "pedaccuracy.meta",
                "loadouts.meta",
                "loader.data",
                "password_is_eulen.rar",
                "abc.abc",
                "loader.vmp.exe",
                "senha_monster.rar",
                "p5m_free.ini",
                "settings.cock",
                "public.zip",
                "debug_logs",
                "quickenter.rpf",
                "municao_infinita.rpf",
                "1_tiro_by_cz.rpf",
                "stamina-DDW.rpf",
                "norecoil-DDW.rpf",
                "fastladder-DDW.rpf",
                "FAST_STRAFE_BY_OSTEN_1.rpf",
                "FAST_STRAFE_BY_OSTEN_1.rar",
                "DopeAmbulance.rpf",
                "DopeTaxi.rpf",
                "DopeBmx_.rpf",
                "maxrange.rar",
                "rage.rpf",
                "WeaponVehicles.rpf",
                "quickEnter-DDW.rpf",
                "HardAmmo-DDW.rpf",
                "damageboost-10.0X-DDW.rpf",
                "damageboost-1.5X-DDW.rpf",
                "Versao_Nova_Citizen_1_tiro_by_frz.rpf",
                "NO_RECOIL_byitalo22.rpf",
                "ZC-softAim.rpf",
                "youngtheuz_skills.rpf",
                "Remove_Roll.rpf",
                "varartirobybruxo.rpf",
                "Varartirobyfrz.rpf",
                "CR-fastRun.rpf",
                "infinitestaminafastreload.rpf",
                "IR.rpf",
                "ZC-infiniteAmmo.rpf",
                "ZC-weaponModifier.rpf",
                "ZC-bulletPenetration.rpf",
                "ZC-increasedRange.rpf",
                "ZC-damageBoost_1.5X.rpf",
                "ZC-damageBoost_10X.rpf",
                "handlingModifier.rpf",
                "ZC-stamina.rpf",
                "handling.rpf",
                "BP.rpf",
                "add_weapon_pistol.50.rpf",
                "pedaccuracy.meta",
                "x64a.rpf",
                "imgui.ini",
            };
            List<string> stringsDesejadasDnscache = new List<string>
            {    //skript
                "skript.gg",
                "skript.gg0",
                "api.projectcheats.com",
                "keyauth.win",
                "https://pki.goog/repository/0",
                 //gosth
                "pedrin.cc",
                "three.pedrin.cc",
                "pedrin.cc",
                "pedrin.ovh",
                "pedrin.cc0!",
                "gosth.gg",
                "api-three.pedrin.cc",
                "ovh-01.pedrin.cc",
                "api-three.pedrin.cc",
                "131.196.198.50",

                //bypass monesy - tracks
                 "api.monesy.dev",
                 "monesy.dev",
                 "api.idandev.xyz",
                 "idandev.xyz", 

                 //redengine
                 "redengine.eu",
            };
            List<string> stringsDesejadasHistorico = new List<string>
            {
            "projectcheats.com",
            "gosth.gg",
            "api.projectcheats.com",
            "skript.gg",
            "skript.gg/favicon.png",
            "cdn.gosth.ltd",
            "redengine.eu/clientarea/download?",
            "xerecao.com.br",
            "tzproject.com",
            "testo.gg",
            };
            Console.WriteLine("Itens do dicionário:");
            foreach (var item in nomesPersonalizadosLsass)
            {
                Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
            }
            foreach (var item in nomesPersonalizadosDps)
            {
                Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
            }
            foreach (var item in nomesPersonalizadosDiagtrack)
            {
                Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
            }
            foreach (var item in nomesPersonalizadosSysmain)
            {
                Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
            }
            foreach (var item in nomesPersonalizadosPcasvc)
            {
                Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
            }
            string informacoesEncontradasExplorer = "";
            if (System.IO.File.Exists("C:\\explorer.txt"))
            {
                string contents = System.IO.File.ReadAllText("C:\\explorer.txt");

                HashSet<string> stringsEncontradasExplorer = new HashSet<string>();

                foreach (string stringDesejadaExplorer in stringsDesejadasExplorer)
                {
                    int indexInicio = contents.IndexOf(stringDesejadaExplorer);
                    while (indexInicio != -1)
                    {
                        int indexFim = contents.IndexOfAny(new char[] { ' ', '\n', '\r', ';', ',', '<', '>' }, indexInicio);
                        if (indexFim == -1)
                        {
                            indexFim = contents.Length;
                        }

                        string stringEncontradaExplorer = contents.Substring(indexInicio, indexFim - indexInicio).Trim();
                        stringsEncontradasExplorer.Add(stringEncontradaExplorer);

                        indexInicio = contents.IndexOf(stringDesejadaExplorer, indexFim);
                    }
                }

                string nomesPersonalizadosConcatenadosExplorer = "";
                foreach (string stringEncontradaExplorer in stringsEncontradasExplorer)
                {
                    string nomePersonalizadoExplorer = nomesPersonalizadosExplorer.ContainsKey(stringEncontradaExplorer) ? nomesPersonalizadosExplorer[stringEncontradaExplorer] : stringEncontradaExplorer;

                    nomesPersonalizadosConcatenadosExplorer += nomePersonalizadoExplorer + "\n ";
                }
                nomesPersonalizadosConcatenadosExplorer = nomesPersonalizadosConcatenadosExplorer.TrimEnd(',', ' ', '\n');

                informacoesEncontradasExplorer += "\n " + nomesPersonalizadosConcatenadosExplorer + "\n";
            }
            Console.WriteLine(informacoesEncontradasExplorer);

            string informacoesEncontradasLsass = "";
            if (System.IO.File.Exists("C:\\Lsass.txt"))
            {
                string contents = System.IO.File.ReadAllText("C:\\Lsass.txt");

                List<string> stringsEncontradasLsass = new List<string>();

                foreach (string stringDesejadaLsass in stringsDesejadasLsass)
                {
                    if (contents.Contains(stringDesejadaLsass))
                    {
                        stringsEncontradasLsass.Add(stringDesejadaLsass);
                    }
                }

                string nomesPersonalizadosConcatenadosLsass = "";
                foreach (string stringEncontradaLsass in stringsEncontradasLsass)
                {
                    string nomePersonalizadoLsass = nomesPersonalizadosLsass.ContainsKey(stringEncontradaLsass) ? nomesPersonalizadosLsass[stringEncontradaLsass] : stringEncontradaLsass;

                    nomesPersonalizadosConcatenadosLsass += nomePersonalizadoLsass + "\n ";
                }
                nomesPersonalizadosConcatenadosLsass = nomesPersonalizadosConcatenadosLsass.TrimEnd(',', ' ', '\n');

                informacoesEncontradasLsass += "\n " + nomesPersonalizadosConcatenadosLsass + "\n";
            }
            string informacoesEncontradasDnscache = "";
            if (System.IO.File.Exists("C:\\Dnscache.txt"))
            {
                string contents = System.IO.File.ReadAllText("C:\\dnscache.txt");

                List<string> stringsEncontradasDnscache = new List<string>();

                foreach (string stringDesejadaDnscache in stringsDesejadasDnscache)
                {
                    if (contents.Contains(stringDesejadaDnscache))
                    {
                        stringsEncontradasDnscache.Add(stringDesejadaDnscache);
                    }
                }

                string nomesPersonalizadosConcatenadosDnscache = "";
                foreach (string stringEncontradaDnscache in stringsEncontradasDnscache)
                {
                    string nomePersonalizadoDnscache = nomesPersonalizadosDnscache.ContainsKey(stringEncontradaDnscache) ? nomesPersonalizadosDnscache[stringEncontradaDnscache] : stringEncontradaDnscache;

                    nomesPersonalizadosConcatenadosDnscache += nomePersonalizadoDnscache + "\n ";
                }
                nomesPersonalizadosConcatenadosDnscache = nomesPersonalizadosConcatenadosDnscache.TrimEnd(',', ' ', '\n');

                informacoesEncontradasDnscache += "\n " + nomesPersonalizadosConcatenadosDnscache + "\n";
            }


            string informacoesEncontradasDps = "";
            if (System.IO.File.Exists("C:\\dps.txt"))
            {
                string contents = System.IO.File.ReadAllText("C:\\dps.txt");

                HashSet<string> stringsEncontradasDps = new HashSet<string>();

                foreach (string stringDesejadaDps in stringsDesejadasDps)
                {
                    int indexInicio = contents.IndexOf(stringDesejadaDps);
                    while (indexInicio != -1)
                    {
                        int indexFim = contents.IndexOfAny(new char[] { ' ', '\n', '\r', ';', ',', '<', '>' }, indexInicio);
                        if (indexFim == -1)
                        {
                            indexFim = contents.Length;
                        }

                        string stringEncontradaDps = contents.Substring(indexInicio, indexFim - indexInicio).Trim();
                        stringsEncontradasDps.Add(stringEncontradaDps);

                        indexInicio = contents.IndexOf(stringDesejadaDps, indexFim);
                    }
                }

                string nomesPersonalizadosConcatenadosDps = "";
                foreach (string stringEncontradaDps in stringsEncontradasDps)
                {
                    string nomePersonalizadoDps = nomesPersonalizadosDps.ContainsKey(stringEncontradaDps) ? nomesPersonalizadosDps[stringEncontradaDps] : stringEncontradaDps;

                    nomesPersonalizadosConcatenadosDps += nomePersonalizadoDps + "\n ";
                }
                nomesPersonalizadosConcatenadosDps = nomesPersonalizadosConcatenadosDps.TrimEnd(',', ' ', '\n');

                informacoesEncontradasDps += "\n " + nomesPersonalizadosConcatenadosDps + "\n";
            }
            Console.WriteLine(informacoesEncontradasDps);

            string informacoesEncontradasDiagtrack = "";
            if (System.IO.File.Exists("C:\\Diagtrack.txt"))
            {
                string contents = System.IO.File.ReadAllText("C:\\Diagtrack.txt");

                List<string> stringsEncontradasDiagtrack = new List<string>();

                foreach (string stringDesejadaDiagtrack in stringsDesejadasDiagtrack)
                {
                    if (contents.Contains(stringDesejadaDiagtrack))
                    {
                        stringsEncontradasDiagtrack.Add(stringDesejadaDiagtrack);
                    }
                }

                string nomesPersonalizadosConcatenadosDiagtrack = "";
                foreach (string stringEncontradaDiagtrack in stringsEncontradasDiagtrack)
                {
                    string nomePersonalizadoDiagtrack = nomesPersonalizadosDiagtrack.ContainsKey(stringEncontradaDiagtrack) ? nomesPersonalizadosDiagtrack[stringEncontradaDiagtrack] : stringEncontradaDiagtrack;

                    nomesPersonalizadosConcatenadosDiagtrack += nomePersonalizadoDiagtrack + "\n ";
                }
                nomesPersonalizadosConcatenadosDiagtrack = nomesPersonalizadosConcatenadosDiagtrack.TrimEnd(',', ' ', '\n');

                informacoesEncontradasDiagtrack += "\n " + nomesPersonalizadosConcatenadosDiagtrack + "\n";
            }

            string informacoesEncontradasSysmain = "";
            if (System.IO.File.Exists("C:\\sysmain.txt"))
            {
                string contents = System.IO.File.ReadAllText("C:\\sysmain.txt");

                List<string> stringsEncontradasSysmain = new List<string>();

                foreach (string stringDesejadaSysmain in stringsDesejadasSysmain)
                {
                    string pattern = $@"{Regex.Escape(stringDesejadaSysmain)}-\w+\.pf";
                    MatchCollection matches = Regex.Matches(contents, pattern, RegexOptions.IgnoreCase);

                    foreach (Match match in matches)
                    {
                        if (match.Success)
                        {
                            stringsEncontradasSysmain.Add(match.Value);
                        }
                    }
                }

                string nomesPersonalizadosConcatenadosSysmain = "";
                foreach (string stringEncontradaSysmain in stringsEncontradasSysmain)
                {
                    string nomePersonalizadoSysmain = nomesPersonalizadosSysmain.ContainsKey(stringEncontradaSysmain) ? nomesPersonalizadosSysmain[stringEncontradaSysmain] : stringEncontradaSysmain;

                    nomesPersonalizadosConcatenadosSysmain += nomePersonalizadoSysmain + "\n";
                }
                nomesPersonalizadosConcatenadosSysmain = nomesPersonalizadosConcatenadosSysmain.TrimEnd(',', ' ', '\n');

                informacoesEncontradasSysmain += "\n " + nomesPersonalizadosConcatenadosSysmain + "\n";
            }

            string informacoesEncontradasHistorico = "";
            if (System.IO.File.Exists("C:\\Historico.txt"))
            {
                string contents = System.IO.File.ReadAllText("C:\\Historico.txt");

                List<string> stringsEncontradasHistorico = new List<string>();

                foreach (string stringDesejadaHistorico in stringsDesejadasHistorico)
                {
                    if (contents.Contains(stringDesejadaHistorico))
                    {
                        int indexInicio = contents.IndexOf(stringDesejadaHistorico);
                        if (indexInicio != -1)
                        {
                            int indexFim = contents.IndexOfAny(new char[] { '/', '?', ':', '.' }, indexInicio + stringDesejadaHistorico.Length);
                            if (indexFim != -1)
                            {
                                string stringEncontradaHistorico = contents.Substring(indexInicio, indexFim - indexInicio).Trim();
                                stringsEncontradasHistorico.Add(stringEncontradaHistorico);
                            }
                        }
                    }
                }

                string nomesPersonalizadosConcatenadosHistorico = "";
                foreach (string stringEncontradaHistorico in stringsEncontradasHistorico)
                {
                    string nomePersonalizadoHistorico = nomesPersonalizadosHistorico.ContainsKey(stringEncontradaHistorico) ? nomesPersonalizadosHistorico[stringEncontradaHistorico] : stringEncontradaHistorico;

                    nomesPersonalizadosConcatenadosHistorico += nomePersonalizadoHistorico + ", ";
                }
                nomesPersonalizadosConcatenadosHistorico = nomesPersonalizadosConcatenadosHistorico.TrimEnd(',', ' ', '\n');

                informacoesEncontradasHistorico += "\n " + nomesPersonalizadosConcatenadosHistorico + "\n";
            }
            Console.WriteLine(nomesPersonalizadosHistorico);
            string informacoesEncontradasPcasvc = "";
            if (System.IO.File.Exists("C:\\pcasvc.txt"))
            {
                string contents = System.IO.File.ReadAllText("C:\\pcasvc.txt");

                List<string> stringsEncontradasPcasvc = new List<string>();

                foreach (string stringDesejadaPcasvc in stringsDesejadasPcasvc)
                {
                    if (contents.Contains(stringDesejadaPcasvc))
                    {
                        stringsEncontradasPcasvc.Add(stringDesejadaPcasvc);
                    }
                }

                string nomesPersonalizadosConcatenadosPcasvc = "";
                foreach (string stringEncontradaPcasvc in stringsEncontradasPcasvc)
                {
                    string nomePersonalizadoPcasvc = nomesPersonalizadosPcasvc.ContainsKey(stringEncontradaPcasvc) ? nomesPersonalizadosPcasvc[stringEncontradaPcasvc] : stringEncontradaPcasvc;

                    nomesPersonalizadosConcatenadosPcasvc += nomePersonalizadoPcasvc + "\n ";
                }

                nomesPersonalizadosConcatenadosPcasvc = nomesPersonalizadosConcatenadosPcasvc.TrimEnd(',', ' ');

                informacoesEncontradasPcasvc += "\n " + nomesPersonalizadosConcatenadosPcasvc + "\n";
            }
            Console.WriteLine(informacoesEncontradasPcasvc);
            string informacoesPcaclient = "";
            
            if (System.IO.File.Exists("C:\\explorer.txt"))
            {
                string contents = System.IO.File.ReadAllText("C:\\explorer.txt");
                int index = contents.IndexOf(",PcaClient,", StringComparison.OrdinalIgnoreCase);

                while (index != -1)
                {
                    
                    int startIndex = contents.LastIndexOf("\n", index);
                    if (startIndex == -1)
                    {
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex += 1; 
                    }

                   
                    int endIndex = contents.IndexOf("\n", index);
                    if (endIndex == -1)
                    {
                        endIndex = contents.Length;
                    }

                   
                    string line = contents.Substring(startIndex, endIndex - startIndex);

                    
                    string[] parts = line.Split(',');
                    if (parts.Length > 5)
                    {
                        string filePath = parts[5];
                        informacoesPcaclient += $"{filePath}\n\n";
                    }
                    index = contents.IndexOf(",PcaClient,", index + 1, StringComparison.OrdinalIgnoreCase);
                }
                Console.WriteLine(informacoesPcaclient);
            }
            if (!string.IsNullOrEmpty(informacoesEncontradasLsass))
            {


                string edicaoWindows = ObterEdicaoWindows();
                string pinFormatado = $"__{pinGerado}__";
                string HWID = $"__{System.Security.Principal.WindowsIdentity.GetCurrent().User.Value}__";
                string PCNAME = $"__{Dns.GetHostEntry(Environment.MachineName).HostName.ToString()}__";
                string UserS = $"__{Environment.UserName.ToString()}__";
                string currentDateTime = $"__{GetCurrentDateTime()}__";
                string ipv4Address = $"__{GetIPv4Address()}__";
                string windowsinstall = $"__{GetWindowsInstallDate()}__";
                string DetectedLsass = $"```{informacoesEncontradasLsass}```";
                string DetectedDps = $"```{informacoesEncontradasDps}```";
                string DetectedDiagtrack = $"```{informacoesEncontradasDiagtrack}```";
                string DetectedSysmain = $"```{informacoesEncontradasSysmain}```";
                string DetectedDnscache = $"```{informacoesEncontradasDnscache}```";
                string DetectedExplorer = $"```{informacoesEncontradasExplorer}```";
                string DetectedHistorico = $"```{informacoesEncontradasHistorico}```";
                string DetectedPcasvc = $"```{informacoesEncontradasPcasvc}```";
                string Pcaclient = $"```{informacoesPcaclient}```";



                string[] serviceNames = { "Sysmain", "Pcasvc", "DPS", "Diagtrack" };

                string resultString = GetServiceStatusString(serviceNames);

                var message = new DiscordMessage()
                    .SetUsername("React Scanner")
                    .SetAvatar("https://cdn.discordapp.com/attachments/1239067249726193686/1252675990304329889/reacticone.png?ex=667314ed&is=6671c36d&hm=7ae5c47ea01dabe43ef8ec5709f621f78afd5ddacb08ef923a6159c0faec6b0d&")
                    .AddEmbed()
                        .SetTimestamp(DateTime.Now)
                        .SetTitle("\nReact Scanner - Cheat")
                        .SetDescription(
                            "> **User:** " + UserS +
                            "\n > **Pc Name:** " + PCNAME +
                            "\n > **Data:** " + currentDateTime +
                            "\n > **Sistema operacional: ** " + edicaoWindows +
                            "\n > **Windows Instalado desde: ** " + windowsinstall +
                            //"\n > **IP:** " + ipv4Address +
                            //"\n > **HWID:** " + HWID +
                            "\n > **Pin:** " + pinFormatado + "\n" +
                            "\n > **Pc Start Time:** " + $"__{GetSystemStartTimeAsString()}__" + "\n" + $"{resultString}" +
                            "\n > **Lsass:**" + "\n" + DetectedLsass +
                             "\n > **Explorer:**" + "\n" + DetectedExplorer +
                            "\n > **DPS:**" + "\n" + DetectedDps +
                            "\n > **Sysmain:**" + "\n" + DetectedSysmain +
                            "\n > **DnsCache:**" + "\n" + DetectedDnscache +
                            "\n > **DiagTrack:**" + "\n" + DetectedDiagtrack +
                            "\n > **PcaSvc:**" + "\n" + DetectedPcasvc +
                            "\n > **Web History:**" + "\n" + DetectedHistorico +
                            "\n > **Pcaclient:**" + "\n" + Pcaclient

                        )
                        .SetColor(00000000)
                        .SetFooter("React Scanner - Logs", "https://cdn.discordapp.com/attachments/1239067249726193686/1252675990304329889/reacticone.png?ex=667314ed&is=6671c36d&hm=7ae5c47ea01dabe43ef8ec5709f621f78afd5ddacb08ef923a6159c0faec6b0d&")
                        .Build();



                message.SendMessage(Result);

            }
            else
            {

                string edicaoWindows = ObterEdicaoWindows();
                string pinFormatado = $"||{pinGerado}||";
                string HWID = $"__{System.Security.Principal.WindowsIdentity.GetCurrent().User.Value}__";
                string PCNAME = $"__{Dns.GetHostEntry(Environment.MachineName).HostName.ToString()}__";
                string UserS = $"__{Environment.UserName.ToString()}__";
                string currentDateTime = $"__{GetCurrentDateTime()}__";
                string ipv4Address = $"__{GetIPv4Address()}__";
                string Pcaclient = $"```{informacoesPcaclient}```";


                var message = new DiscordMessage()
                   .SetUsername("React Scanner - Clean")
                   .SetAvatar("https://cdn.discordapp.com/attachments/1239067249726193686/1252675990304329889/reacticone.png?ex=667314ed&is=6671c36d&hm=7ae5c47ea01dabe43ef8ec5709f621f78afd5ddacb08ef923a6159c0faec6b0d&")
                   .AddEmbed()
                       .SetTimestamp(DateTime.Now)
                .SetTitle("\nReact Scanner")
                       .SetDescription(
                           "> **User:** " + UserS +
                            "\n > **Pc Name:** " + PCNAME +
                            "\n > **Data:** " + currentDateTime +
                            "\n > **Sistema operacional: ** " + edicaoWindows +

                            // "\n > **IP:** " + ipv4Address +
                            //"\n > **HWID:** " + HWID +
                            "\n > **Pin:** " + pinFormatado +
                           "\n > **Resultado** " + $"__{"Clean"}__" +
                            "\n > **PcaClient:** " + Pcaclient


                       )
                       .SetColor(00000000)
                       .SetFooter("React Scanner - Logs", "https://cdn.discordapp.com/attachments/1239067249726193686/1252675990304329889/reacticone.png?ex=667314ed&is=6671c36d&hm=7ae5c47ea01dabe43ef8ec5709f621f78afd5ddacb08ef923a6159c0faec6b0d&")
                       .Build();



                message.SendMessage(Result);
            }


            Thread.Sleep(3000);
            string[] filesToDelete = { @"C:\dnscache.txt", @"C:\Historico.txt", @"C:\explorer.txt", @"C:\strings.exe", @"C:\diagtrack.txt", @"C:\PcaSvc.txt", @"C:\sysmain.txt", @"C:\DPS.txt", @"C:\Lsass.txt", @"C:\Discord.txt" };

            foreach (string filePath in filesToDelete)
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

        }

        public void Eventvwr()
        {
            string outputFilePath1000 = @"C:\FilteredEventLog1000.txt";
            string outputFilePath1002 = @"C:\FilteredEventLog1002.txt";

            List<string> filteredLogEntries1000 = ReadAndFilterEventLogs(1000, outputFilePath1000);
            List<string> furtherFilteredLogEntries1000 = ExtractLogEntriesFromEvent1000(outputFilePath1000);
            List<string> filteredLogEntries1002 = ReadAndFilterEventLogs(1002, outputFilePath1002);

            var message1000 = CreateWebhookMessage(furtherFilteredLogEntries1000, "1000");
            var message1002 = CreateWebhookMessage(filteredLogEntries1002, "1002");

            SendMessageToWebhook(message1000);
            SendMessageToWebhook(message1002);
        }

        private List<string> ReadAndFilterEventLogs(int eventId, string outputFilePath)
        {
            List<string> filteredLogEntries = new List<string>();

            try
            {
                string query = eventId == 1000 ? "*[System[EventID=1000]]" : "*[System[EventID=1002]]";
                EventLogQuery eventLogQuery = new EventLogQuery("Application", PathType.LogName, query);
                EventLogReader logReader = new EventLogReader(eventLogQuery);
                Regex regex = eventId == 1000
                    ? new Regex(@"^(Horario:|Nome do aplicativo com falha:|Nome do módulo com falha:|Caminho do aplicativo com falha:|Caminho do módulo com falha:) (.+)", RegexOptions.Multiline)
                    : new Regex(@"shell", RegexOptions.IgnoreCase);

                List<string> logEntries = new List<string>();

                for (EventRecord eventRecord = logReader.ReadEvent(); eventRecord != null; eventRecord = logReader.ReadEvent())
                {
                    string timeCreated = eventRecord.TimeCreated.HasValue ? eventRecord.TimeCreated.Value.ToString("dd/MM/yyyy HH:mm:ss") : "Unknown";
                    string formatDescription = eventRecord.FormatDescription();

                    if (eventId == 1000 && regex.IsMatch(formatDescription))
                    {
                        var matches = regex.Matches(formatDescription).Cast<Match>().Select(m => m.Value.Trim());
                        string logEntry = $"Horario: {timeCreated}\n{string.Join("\n", matches)}\n";
                        logEntries.Add(logEntry);
                    }
                    else if (eventId == 1002 && formatDescription.Contains("shell"))
                    {
                        logEntries.Add($"Horario: {timeCreated}\n{formatDescription.Trim()}\n");
                    }
                }

                logEntries.Reverse();

                int count = Math.Min(logEntries.Count, 3);

                for (int i = 0; i < count; i++)
                {
                    filteredLogEntries.Add(logEntries[i]);
                }

                System.IO.File.WriteAllLines(outputFilePath, filteredLogEntries);
                Console.WriteLine($"Logs filtrados com ID {eventId} salvos em: {outputFilePath}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Erro de acesso: {ex.Message}");
            }
            catch (EventLogException ex)
            {
                Console.WriteLine($"Erro ao ler os logs de eventos: {ex.Message}");
            }

            return filteredLogEntries;
        }

        private List<string> ExtractLogEntriesFromEvent1000(string logFilePath)
        {
            List<string> extractedEntries = new List<string>();

            try
            {
                if (!System.IO.File.Exists(logFilePath))
                {
                    Console.WriteLine($"Erro: Arquivo não encontrado: {logFilePath}");
                    return extractedEntries;
                }

                string[] lines = System.IO.File.ReadAllLines(logFilePath);

                string horario = "";
                string nomeAplicativo = "";
                string nomeModulo = "";
                string caminhoAplicativo = "";
                string caminhoModulo = "";

                foreach (string line in lines)
                {
                    if (line.StartsWith("Horario:"))
                    {
                        horario = line.Replace("Horario: ", "");
                    }
                    else if (line.StartsWith("Nome do aplicativo com falha:"))
                    {
                        nomeAplicativo = GetExecutableName(line);
                    }
                    else if (line.StartsWith("Nome do módulo com falha:"))
                    {
                        nomeModulo = GetModuleName(line);
                    }
                    else if (line.StartsWith("Caminho do aplicativo com falha:"))
                    {
                        caminhoAplicativo = GetExecutablePath(line);
                    }
                    else if (line.StartsWith("Caminho do módulo com falha:"))
                    {
                        caminhoModulo = GetModulePath(line);
                    }
                    else if (string.IsNullOrWhiteSpace(line))
                    {
                        string entry = $"Horario: {horario}\n" +
                                       $"**Nome do aplicativo com falha**: {nomeAplicativo}\n" +
                                       $"Nome do Modulo com falha: {nomeModulo}\n\n" +
                                       $"Caminho do Modulo com falha: {caminhoModulo}\n\n"+
                                       $"Caminho do Aplicativo com falha: {caminhoAplicativo}\n\n";




                        extractedEntries.Add(entry);
                        horario = "";
                        nomeAplicativo = "";
                        nomeModulo = "";
                        caminhoModulo = "";
                        caminhoAplicativo = "";
                    }
                }

                System.IO.File.WriteAllLines(logFilePath, extractedEntries);
                Console.WriteLine($"Logs filtrados salvos em: {logFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao extrair entradas de log: {ex.Message}");
            }

            return extractedEntries;
        }

        private string GetExecutableName(string line)
        {
            string[] parts = line.Split(new[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
            string nomeAplicativo = parts.Length > 1 ? parts[1].Trim() : "";

            int index = nomeAplicativo.IndexOf(".exe");
            if (index != -1)
            {
                nomeAplicativo = nomeAplicativo.Substring(0, index + 4);
            }

            return nomeAplicativo;
        }

        private string GetModuleName(string line)
        {
            string[] parts = line.Split(new[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
            string NomeModulo = "";

            if (parts.Length > 1)
            {
                string[] moduleInfo = parts[1].Split(',');
                NomeModulo = moduleInfo[0].Trim();
            }

            return NomeModulo;
        }

        private string GetExecutablePath(string line)
        {
            string[] parts = line.Split(new[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
            string caminhoAplicativo = parts.Length > 1 ? parts[1].Trim() : "";

            int index = caminhoAplicativo.IndexOf(".exe");
            if (index != -1)
            {
                caminhoAplicativo = caminhoAplicativo.Substring(0, index + 4);
            }
            else
            {
                caminhoAplicativo = "Inexistente";
            }

            return caminhoAplicativo;
        }

        private string GetModulePath(string line)
        {
            string[] parts = line.Split(new[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
            string caminhoModulo = parts.Length > 1 ? parts[1].Trim() : "";

            int indexExe = caminhoModulo.IndexOf(".exe");
            int indexDll = caminhoModulo.IndexOf(".dll");

            if (indexExe != -1 || indexDll != -1)
            {
                int index = indexExe != -1 ? indexExe : indexDll;
                caminhoModulo = caminhoModulo.Substring(0, index + 4);
            }
            else
            {
                caminhoModulo = "Inexistente";
            }

            return caminhoModulo;
        }

        private DiscordMessage CreateWebhookMessage(List<string> filteredLogEntries, string eventId)
        {
            string messageContent = string.Join("\n\n", filteredLogEntries);
            string title = eventId == "1000" ? "Event 1000:" : "Event 1002:";

            var message = new DiscordMessage()
                .SetUsername("Event Log - React Scanner")
                .SetAvatar("https://cdn.discordapp.com/attachments/1239067249726193686/1252674833582526568/image.png?ex=667313d9&is=6671c259&hm=615ad1821d51830ea2bda0f07c81aba66f599668579f687aca688fec727edba0&")
                .AddEmbed()
                    .SetTimestamp(DateTime.Now)
                    .SetTitle("\nEvent Log - React Scanner")
                    .SetDescription($"**{title}**\n```{messageContent}```")
                    .SetColor(0x008B8B)
                    .SetFooter("EventLog", "https://cdn.discordapp.com/attachments/1239067249726193686/1252674833582526568/image.png?ex=667313d9&is=6671c259&hm=615ad1821d51830ea2bda0f07c81aba66f599668579f687aca688fec727edba0&")
                    .Build();

            return message;


        }

        private void SendMessageToWebhook(DiscordMessage message)
        {
            try
            {
                message.SendMessage(Eventlog);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar mensagem para a webhook: {ex.Message}");
            }
            Thread.Sleep(3000);
            string[] filesToDelete = {"C:/FilteredEventLog1002.txt", @"C:/FilteredEventLog1000.txt"};

            foreach (string filePath in filesToDelete)
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }
        static void AdicionarItemAoDicionario(Dictionary<string, string> dicionario, string chave, string valor)
        {
            if (!dicionario.ContainsKey(chave))
            {
                dicionario.Add(chave, valor);
                Console.WriteLine($"Adicionado: Chave '{chave}', Valor '{valor}'");
            }
            else
            {
                Console.WriteLine($"Chave '{chave}' já existe, com '{valor}' pulando...");
            }
        }
        private string ObterEdicaoWindows()
        {
            const string chaveRegistro = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
            const string valorChave = "ProductName";

            using (RegistryKey chave = Registry.LocalMachine.OpenSubKey(chaveRegistro))
            {
                if (chave != null)
                {
                    object valor = chave.GetValue(valorChave);

                    if (valor != null)
                    {
                        return valor.ToString();
                    }
                }
            }

            return "Edição não encontrada";
        }
        static string GetServiceStatusString(string[] serviceNames)
        {
            string result = "";

            foreach (var serviceName in serviceNames)
            {
                string serviceStatus = GetServiceStatus(serviceName);

                result += $"{serviceStatus}\n";
            }

            return result;
        }


        static string GetServiceStatus(string serviceName)
        {
            try
            {
                using (ServiceController sc = new ServiceController(serviceName))
                {
                    return $" > **{serviceName}** : __{sc.Status.ToString().ToLower()}__";
                }
            }
            catch (Exception ex)
            {
                return $" > **{serviceName}** : Erro ao obter status: {ex.Message.ToLower()}";
            }
        }


        static string GetSystemStartTimeAsString()
        {
            DateTime systemStartTime = GetSystemStartTime();
            return systemStartTime.ToString("dd/MM/yyyy - HH:mm:ss");
        }
        static DateTime GetSystemStartTime()
        {

            using (PerformanceCounter uptimeCounter = new PerformanceCounter("System", "System Up Time"))
            {
                uptimeCounter.NextValue();

                float uptimeSeconds = uptimeCounter.NextValue();

                DateTime systemStartTime = DateTime.Now - TimeSpan.FromSeconds(uptimeSeconds);

                return systemStartTime;
            }
        }

        static DateTime GetWindowsInstallDate()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                if (key != null)
                {
                    object installDateValue = key.GetValue("InstallDate", 0);

                    DateTime installDate = DateTime.MinValue;
                    if (installDateValue is int)
                    {
                        int secondsSince1970 = (int)installDateValue;
                        installDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(secondsSince1970).ToLocalTime();
                    }

                    return installDate;
                }
                else
                {
                    throw new Exception("Chave do Registro não encontrada.");
                }
            }
        }
        private bool localizacaoMudada = false;

        private async void siticoneTextBox2_TextChanged(object sender, EventArgs e)
        {
            siticoneTextBox2.TextAlign = HorizontalAlignment.Center;
            await Task.Run(() =>
            {
                if (siticoneTextBox2.Text.Length == 5 && siticoneTextBox2.Text == pinGerado)
                {
                    label2.Invoke((MethodInvoker)(() =>
                    {
                        label2.Text = "Pin Validado com\n Sucesso !";
                        label2.ForeColor = Color.Green;
                        label2.AutoSize = true;
                        label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        label2.Left = (this.ClientSize.Width - label2.Width) / 2;
                    }));
                    Thread.Sleep(1000);

                    siticoneTextBox2.Invoke((MethodInvoker)(() =>
                    {
                        siticoneTextBox2.Visible = false;
                    }));

                    if (!localizacaoMudada)
                    {
                        localizacaoMudada = true;
                        label2.Invoke((MethodInvoker)(() =>
                        {
                            label2.Text = "Loading...";
                            label2.ForeColor = Color.LightCyan;
                            label2.AutoSize = true;
                            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
                            label2.Location = new Point(310, 190);
                        }));

                        siticoneVProgressBar1.Invoke((MethodInvoker)(() =>
                        {
                            siticoneVProgressBar1.Minimum = 0;
                            siticoneVProgressBar1.Maximum = 100;
                            siticoneVProgressBar1.Value = 0;
                            siticoneVProgressBar1.Visible = true;
                        }));

                        int totalSteps = 4;

                        for (int step = 1; step <= totalSteps; step++)
                        {
                            switch (step)
                            {
                                case 1:
                                    StringColetor();
                                    break;
                                case 2:
                                    coletorzin();
                                    break;
                                case 3:
                                    scanner();
                                    break;
                                case 4:
                                    Eventvwr();
                                    break;
                            }

                            int progress = (step * 100) / totalSteps;
                            siticoneVProgressBar1.Invoke((MethodInvoker)(() =>
                            {
                                siticoneVProgressBar1.Value = progress;
                            }));
                        }

                        label2.Invoke((MethodInvoker)(() =>
                        {
                            label2.Text = "Completo!";
                            label2.ForeColor = System.Drawing.Color.DarkTurquoise;
                            label2.AutoSize = true;
                            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
                            label2.Location = new Point(310, 210);
                            label2.Font = new Font(label2.Font.FontFamily, 15);
                        }));

                        siticoneVProgressBar1.Invoke((MethodInvoker)(() =>
                        {
                            siticoneVProgressBar1.Visible = false;
                        }));
                    }
                }
                else if (siticoneTextBox2.Text.Length == 5)
                {
                    label2.Invoke((MethodInvoker)(() =>
                    {
                        label2.Text = "Pin não Autorizado";
                        label2.ForeColor = Color.Red;
                        label2.AutoSize = true;
                        label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        label2.Left = (this.ClientSize.Width - label2.Width) / 2;
                    }));

                    siticoneTextBox2.Invoke((MethodInvoker)(() =>
                    {
                        siticoneTextBox2.Text = string.Empty;
                    }));
                }
            });
        }
        static string GerarPin()
        {
            Random random = new Random();
            int pin = random.Next(10000, 99999);
            return pin.ToString();
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            StoogeLeaks();
            Invalidate(); 
        }
        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPosition = e.Location;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newLocation = this.Location;
                newLocation.X += e.X - lastCursorPosition.X;
                newLocation.Y += e.Y - lastCursorPosition.Y;
                this.Location = newLocation;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
    private void siticonePictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void Resposta_Click(object sender, EventArgs e)
        {

        }

        private void Resposta_Click_1(object sender, EventArgs e)
        {

        }

        private void siticoneButton2_Click_1(object sender, EventArgs e)
        {


        }

        private void siticoneVProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void siticoneButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
    public class Particle
    {
        public PointF Position { get; set; }
        public PointF Velocity { get; set; }
        public int Radius { get; set; }
        public System.Drawing.Color Color { get; set; }
    }


