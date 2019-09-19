using System;
using System.Collections.Generic;
using System.Drawing;
using Library;
using Library.SystemModels;
using MirDB;
using Server.Models;

namespace Server.DBModels
{
    [UserObject]
    public sealed class CharacterInfo : DBObject
    {
        [Association("Characters")]
        public AccountInfo Account
        {
            get { return _Account; }
            set
            {
                if (_Account == value) return;

                var oldValue = _Account;
                _Account = value;

                OnChanged(oldValue, value, "Account");
            }
        }
        private AccountInfo _Account;
        

        public string CharacterName
        {
            get { return _CharacterName; }
            set
            {
                if (_CharacterName == value) return;

                var oldValue = _CharacterName;
                _CharacterName = value;

                OnChanged(oldValue, value, "CharacterName");
            }
        }
        private string _CharacterName;

        public MirClass Class
        {
            get { return _Class; }
            set
            {
                if (_Class == value) return;

                var oldValue = _Class;
                _Class = value;

                OnChanged(oldValue, value, "Class");
            }
        }
        private MirClass _Class;

        public MirGender Gender
        {
            get { return _Gender; }
            set
            {
                if (_Gender == value) return;

                var oldValue = _Gender;
                _Gender = value;

                OnChanged(oldValue, value, "Gender");
            }
        }
        private MirGender _Gender;

        public int Level
        {
            get { return _Level; }
            set
            {
                if (_Level == value) return;

                var oldValue = _Level;
                _Level = value;

                OnChanged(oldValue, value, "Level");
            }
        }
        private int _Level;

        public int HairType
        {
            get { return _HairType; }
            set
            {
                if (_HairType == value) return;

                var oldValue = _HairType;
                _HairType = value;

                OnChanged(oldValue, value, "HairType");
            }
        }
        private int _HairType;

        public Color HairColour
        {
            get { return _HairColour; }
            set
            {
                if (_HairColour == value) return;

                var oldValue = _HairColour;
                _HairColour = value;

                OnChanged(oldValue, value, "HairColour");
            }
        }
        private Color _HairColour;

        public Color ArmourColour
        {
            get { return _ArmourColour; }
            set
            {
                if (_ArmourColour == value) return;

                var oldValue = _ArmourColour;
                _ArmourColour = value;

                OnChanged(oldValue, value, "ArmourColour");
            }
        }
        private Color _ArmourColour;
        
        public DateTime LastLogin
        {
            get { return _LastLogin; }
            set
            {
                if (_LastLogin == value) return;

                var oldValue = _LastLogin;
                _LastLogin = value;

                OnChanged(oldValue, value, "LastLogin");
            }
        }
        private DateTime _LastLogin;

        public bool Deleted
        {
            get { return _Deleted; }
            set
            {
                if (_Deleted == value) return;

                var oldValue = _Deleted;
                _Deleted = value;

                OnChanged(oldValue, value, "Deleted");
            }
        }
        private bool _Deleted;

        public DateTime CreationDate
        {
            get { return _CreationDate; }
            set
            {
                if (_CreationDate == value) return;

                var oldValue = _CreationDate;
                _CreationDate = value;

                OnChanged(oldValue, value, "CreationDate");
            }
        }
        private DateTime _CreationDate;

        public string CreationIP
        {
            get { return _CreationIP; }
            set
            {
                if (_CreationIP == value) return;

                var oldValue = _CreationIP;
                _CreationIP = value;

                OnChanged(oldValue, value, "CreationIP");
            }
        }
        private string _CreationIP;

        public Point CurrentLocation
        {
            get { return _CurrentLocation; }
            set
            {
                if (_CurrentLocation == value) return;

                var oldValue = _CurrentLocation;
                _CurrentLocation = value;

                OnChanged(oldValue, value, "CurrentLocation");
            }
        }
        private Point _CurrentLocation;
        
        public MapInfo CurrentMap
        {
            get { return _CurrentMap; }
            set
            {
                if (_CurrentMap == value) return;

                var oldValue = _CurrentMap;
                _CurrentMap = value;

                OnChanged(oldValue, value, "CurrentMap");
            }
        }
        private MapInfo _CurrentMap;

        public MirDirection Direction
        {
            get { return _Direction; }
            set
            {
                if (_Direction == value) return;

                var oldValue = _Direction;
                _Direction = value;

                OnChanged(oldValue, value, "Direction");
            }
        }
        private MirDirection _Direction;
        
        public SafeZoneInfo BindPoint
        {
            get { return _BindPoint; }
            set
            {
                if (_BindPoint == value) return;

                var oldValue = _BindPoint;
                _BindPoint = value;

                OnChanged(oldValue, value, "BindPoint");
            }
        }
        private SafeZoneInfo _BindPoint;

        public int CurrentHP
        {
            get { return _CurrentHP; }
            set
            {
                if (_CurrentHP == value) return;

                var oldValue = _CurrentHP;
                _CurrentHP = value;

                OnChanged(oldValue, value, "CurrentHP");
            }
        }
        private int _CurrentHP;

        public int CurrentMP
        {
            get { return _CurrentMP; }
            set
            {
                if (_CurrentMP == value) return;

                var oldValue = _CurrentMP;
                _CurrentMP = value;

                OnChanged(oldValue, value, "CurrentMP");
            }
        }
        private int _CurrentMP;

        public decimal Experience
        {
            get { return _Experience; }
            set
            {
                if (_Experience == value) return;

                var oldValue = _Experience;
                _Experience = value;

                OnChanged(oldValue, value, "Experience");
            }
        }
        private decimal _Experience;

        public bool CanThrusting
        {
            get { return _canThrusting; }
            set
            {
                if (_canThrusting == value) return;

                var oldValue = _canThrusting;
                _canThrusting = value;

                OnChanged(oldValue, value, "CanThrusting");
            }
        }
        private bool _canThrusting;

        public bool CanHalfMoon
        {
            get { return _CanHalfMoon; }
            set
            {
                if (_CanHalfMoon == value) return;

                var oldValue = _CanHalfMoon;
                _CanHalfMoon = value;

                OnChanged(oldValue, value, "CanHalfMoon");
            }
        }
        private bool _CanHalfMoon;

        public bool CanDestructiveSurge
        {
            get { return _canDestructiveSurge; }
            set
            {
                if (_canDestructiveSurge == value) return;

                var oldValue = _canDestructiveSurge;
                _canDestructiveSurge = value;

                OnChanged(oldValue, value, "CanDestructiveSurge");
            }
        }
        private bool _canDestructiveSurge;

        public bool CanFlameSplash
        {
            get { return _CanFlameSplash; }
            set
            {
                if (_CanFlameSplash == value) return;

                var oldValue = _CanFlameSplash;
                _CanFlameSplash = value;

                OnChanged(oldValue, value, "CanFlameSplash");
            }
        }
        private bool _CanFlameSplash;
        
        public Stats LastStats
        {
            get { return _LastStats; }
            set
            {
                if (_LastStats == value) return;

                var oldValue = _LastStats;
                _LastStats = value;

                OnChanged(oldValue, value, "LastStats");
            }
        }
        private Stats _LastStats;

        public Stats HermitStats
        {
            get { return _HermitStats; }
            set
            {
                if (_HermitStats == value) return;

                var oldValue = _HermitStats;
                _HermitStats = value;

                OnChanged(oldValue, value, "HermitStats");
            }
        }
        private Stats _HermitStats;

        public int SpentPoints
        {
            get { return _SpentPoints; }
            set
            {
                if (_SpentPoints == value) return;

                var oldValue = _SpentPoints;
                _SpentPoints = value;

                OnChanged(oldValue, value, "SpentPoints");
            }
        }
        private int _SpentPoints;

        public AttackMode AttackMode
        {
            get { return _AttackMode; }
            set
            {
                if (_AttackMode == value) return;

                var oldValue = _AttackMode;
                _AttackMode = value;

                OnChanged(oldValue, value, "AttackMode");
            }
        }
        private AttackMode _AttackMode;

        public PetMode PetMode
        {
            get { return _PetMode; }
            set
            {
                if (_PetMode == value) return;

                var oldValue = _PetMode;
                _PetMode = value;

                OnChanged(oldValue, value, "PetMode");
            }
        }
        private PetMode _PetMode;
        
        public bool Observable
        {
            get { return _Observable; }
            set
            {
                if (_Observable == value) return;

                var oldValue = _Observable;
                _Observable = value;

                OnChanged(oldValue, value, "Observable");
            }
        }
        private bool _Observable;

        public DateTime ItemReviveTime
        {
            get { return _ItemReviveTime; }
            set
            {
                if (_ItemReviveTime == value) return;

                var oldValue = _ItemReviveTime;
                _ItemReviveTime = value;

                OnChanged(oldValue, value, "ItemReviveTime");
            }
        }
        private DateTime _ItemReviveTime;

        public DateTime ReincarnationPillTime
        {
            get { return _ReincarnationPillTime; }
            set
            {
                if (_ReincarnationPillTime == value) return;

                var oldValue = _ReincarnationPillTime;
                _ReincarnationPillTime = value;

                OnChanged(oldValue, value, "ReincarnationPillTime");
            }
        }
        private DateTime _ReincarnationPillTime;

        public DateTime MarriageTeleportTime
        {
            get { return _MarriageTeleportTime; }
            set
            {
                if (_MarriageTeleportTime == value) return;

                var oldValue = _MarriageTeleportTime;
                _MarriageTeleportTime = value;

                OnChanged(oldValue, value, "MarriageTeleportTime");
            }
        }
        private DateTime _MarriageTeleportTime;

        public DateTime GroupRecallTime
        {
            get { return _GroupRecallTime; }
            set
            {
                if (_GroupRecallTime == value) return;

                var oldValue = _GroupRecallTime;
                _GroupRecallTime = value;

                OnChanged(oldValue, value, "GroupRecallTime");
            }
        }
        private DateTime _GroupRecallTime;
        
        public bool HideHelmet
        {
            get { return _HideHelmet; }
            set
            {
                if (_HideHelmet == value) return;

                var oldValue = _HideHelmet;
                _HideHelmet = value;

                OnChanged(oldValue, value, "HideHelmet");
            }
        }
        private bool _HideHelmet;

        public bool CanDeathDrop
        {
            get { return _CanDeathDrop; }
            set
            {
                if (_CanDeathDrop == value) return;

                var oldValue = _CanDeathDrop;
                _CanDeathDrop = value;

                OnChanged(oldValue, value, "CanDeathDrop");
            }
        }
        private bool _CanDeathDrop;
        
        public int Rebirth
        {
            get => _Rebirth;
            set
            {
                if (_Rebirth == value) return;

                int oldValue = _Rebirth;
                _Rebirth = value;

                OnChanged(oldValue, value, "Rebirth");
            }
        }
        private int _Rebirth;

        


        public DateTime NextDeathDropChange
        {
            get { return _NextDeathDropChange; }
            set
            {
                if (_NextDeathDropChange == value) return;

                var oldValue = _NextDeathDropChange;
                _NextDeathDropChange = value;

                OnChanged(oldValue, value, "NextDeathDropChange");
            }
        }
        private DateTime _NextDeathDropChange;
        
        
        
        [Association("Companion")]
        public UserCompanion Companion
        {
            get { return _Companion; }
            set
            {
                if (_Companion == value) return;

                var oldValue = _Companion;
                _Companion = value;

                OnChanged(oldValue, value, "Companion");
            }
        }
        private UserCompanion _Companion;

        public bool CompanionForbidGold
        {
            get { return _CompanionForbidGold; }
            set
            {
                if (_CompanionForbidGold == value) return;

                var oldValue = _CompanionForbidGold;
                _CompanionForbidGold = value;

                OnChanged(oldValue, value, "CompanionForbidGold");
            }
        }
        private bool _CompanionForbidGold;

        public bool CompanionForbidWeapon
        {
            get { return _CompanionForbidWeapon; }
            set
            {
                if (_CompanionForbidWeapon == value) return;

                var oldValue = _CompanionForbidWeapon;
                _CompanionForbidWeapon = value;

                OnChanged(oldValue, value, "CompanionForbidWeapon");
            }
        }
        private bool _CompanionForbidWeapon;

        public bool CompanionForbidArmour
        {
            get { return _CompanionForbidArmour; }
            set
            {
                if (_CompanionForbidArmour == value) return;

                var oldValue = _CompanionForbidArmour;
                _CompanionForbidArmour = value;

                OnChanged(oldValue, value, "CompanionForbidArmour");
            }
        }
        private bool _CompanionForbidArmour;

        public bool CompanionForbidHelmet
        {
            get { return _CompanionForbidHelmet; }
            set
            {
                if (_CompanionForbidHelmet == value) return;

                var oldValue = _CompanionForbidHelmet;
                _CompanionForbidHelmet = value;

                OnChanged(oldValue, value, "CompanionForbidHelmet");
            }
        }
        private bool _CompanionForbidHelmet;

        public bool CompanionForbidShield
        {
            get { return _CompanionForbidShield; }
            set
            {
                if (_CompanionForbidShield == value) return;

                var oldValue = _CompanionForbidShield;
                _CompanionForbidShield = value;

                OnChanged(oldValue, value, "CompanionForbidShield");
            }
        }
        private bool _CompanionForbidShield;

        public bool CompanionForbidNecklace
        {
            get { return _CompanionForbidNecklace; }
            set
            {
                if (_CompanionForbidNecklace == value) return;

                var oldValue = _CompanionForbidNecklace;
                _CompanionForbidNecklace = value;

                OnChanged(oldValue, value, "CompanionForbidNecklace");
            }
        }
        private bool _CompanionForbidNecklace;

        public bool CompanionForbidBracelet
        {
            get { return _CompanionForbidBracelet; }
            set
            {
                if (_CompanionForbidBracelet == value) return;

                var oldValue = _CompanionForbidBracelet;
                _CompanionForbidBracelet = value;

                OnChanged(oldValue, value, "CompanionForbidBracelet");
            }
        }
        private bool _CompanionForbidBracelet;

        public bool CompanionForbidRing
        {
            get { return _CompanionForbidRing; }
            set
            {
                if (_CompanionForbidRing == value) return;

                var oldValue = _CompanionForbidRing;
                _CompanionForbidRing = value;

                OnChanged(oldValue, value, "CompanionForbidRing");
            }
        }
        private bool _CompanionForbidRing;

        public bool CompanionForbidShoes
        {
            get { return _CompanionForbidShoes; }
            set
            {
                if (_CompanionForbidShoes == value) return;

                var oldValue = _CompanionForbidShoes;
                _CompanionForbidShoes = value;

                OnChanged(oldValue, value, "CompanionForbidShoes");
            }
        }
        private bool _CompanionForbidShoes;

        public bool CompanionForbidBook
        {
            get { return _CompanionForbidBook; }
            set
            {
                if (_CompanionForbidBook == value) return;

                var oldValue = _CompanionForbidBook;
                _CompanionForbidBook = value;

                OnChanged(oldValue, value, "CompanionForbidBook");
            }
        }
        private bool _CompanionForbidBook;

        public bool CompanionForbidBookWarrior
        {
            get { return _CompanionForbidBookWarrior; }
            set
            {
                if (_CompanionForbidBookWarrior == value) return;

                var oldValue = _CompanionForbidBookWarrior;
                _CompanionForbidBookWarrior = value;

                OnChanged(oldValue, value, "CompanionForbidBookWarrior");
            }
        }
        private bool _CompanionForbidBookWarrior;

        public bool CompanionForbidBookWizard
        {
            get { return _CompanionForbidBookWizard; }
            set
            {
                if (_CompanionForbidBookWizard == value) return;

                var oldValue = _CompanionForbidBookWizard;
                _CompanionForbidBookWizard = value;

                OnChanged(oldValue, value, "CompanionForbidBookWizard");
            }
        }
        private bool _CompanionForbidBookWizard;

        public bool CompanionForbidBookTaoist
        {
            get { return _CompanionForbidBookTaoist; }
            set
            {
                if (_CompanionForbidBookTaoist == value) return;

                var oldValue = _CompanionForbidBookTaoist;
                _CompanionForbidBookTaoist = value;

                OnChanged(oldValue, value, "CompanionForbidBookTaoist");
            }
        }
        private bool _CompanionForbidBookTaoist;

        public bool CompanionForbidBookAssassin
        {
            get { return _CompanionForbidBookAssassin; }
            set
            {
                if (_CompanionForbidBookAssassin == value) return;

                var oldValue = _CompanionForbidBookAssassin;
                _CompanionForbidBookAssassin = value;

                OnChanged(oldValue, value, "CompanionForbidBookAssassin");
            }
        }
        private bool _CompanionForbidBookAssassin;

        public bool CompanionForbidPotion
        {
            get { return _CompanionForbidPotion; }
            set
            {
                if (_CompanionForbidPotion == value) return;

                var oldValue = _CompanionForbidPotion;
                _CompanionForbidPotion = value;

                OnChanged(oldValue, value, "CompanionForbidPotion");
            }
        }
        private bool _CompanionForbidPotion;

        public bool CompanionForbidMeat
        {
            get { return _CompanionForbidMeat; }
            set
            {
                if (_CompanionForbidMeat == value) return;

                var oldValue = _CompanionForbidMeat;
                _CompanionForbidMeat = value;

                OnChanged(oldValue, value, "CompanionForbidMeat");
            }
        }
        private bool _CompanionForbidMeat;

        public bool CompanionForbidCommon
        {
            get { return _CompanionForbidCommon; }
            set
            {
                if (_CompanionForbidCommon == value) return;

                var oldValue = _CompanionForbidCommon;
                _CompanionForbidCommon = value;

                OnChanged(oldValue, value, "CompanionForbidCommon");
            }
        }
        private bool _CompanionForbidCommon;

        public bool CompanionForbidSuperior
        {
            get { return _CompanionForbidSuperior; }
            set
            {
                if (_CompanionForbidSuperior == value) return;

                var oldValue = _CompanionForbidSuperior;
                _CompanionForbidSuperior = value;

                OnChanged(oldValue, value, "CompanionForbidSuperior");
            }
        }
        private bool _CompanionForbidSuperior;

        public bool CompanionForbidElite
        {
            get { return _CompanionForbidElite; }
            set
            {
                if (_CompanionForbidElite == value) return;

                var oldValue = _CompanionForbidElite;
                _CompanionForbidElite = value;

                OnChanged(oldValue, value, "CompanionForbidElite");
            }
        }
        private bool _CompanionForbidElite;

        [Association("Items", true)]
        public DBBindingList<UserItem> Items { get; set; }

        [Association("BeltLinks", true)]
        public DBBindingList<CharacterBeltLink> BeltLinks { get; set; }

        [Association("AutoPotionLinks", true)]
        public DBBindingList<AutoPotionLink> AutoPotionLinks { get; set; }

        [Association("Magics", true)]
        public DBBindingList<UserMagic> Magics { get; set; }

        [Association("Buffs", true)]
        public DBBindingList<BuffInfo> Buffs { get; set; }
        
        [Association("Refines", true)]
        public DBBindingList<RefineInfo> Refines { get; set; }

        [Association("Quests", true)]
        public DBBindingList<UserQuest> Quests { get; set; }

        [Association("Marriage")]
        public CharacterInfo Partner
        {
            get { return _Partner; }
            set
            {
                if (_Partner == value) return;

                var oldValue = _Partner;
                _Partner = value;

                OnChanged(oldValue, value, "Partner");
            }
        }
        private CharacterInfo _Partner;

        [Association("Master")]
        public CharacterInfo MasterCharacter
        {
            get { return _MasterCharacter; }
            set
            {
                if (_MasterCharacter == value) return;

                var oldValue = _MasterCharacter;
                _MasterCharacter = value;

                OnChanged(oldValue, value, "MasterCharacter");
            }
        }
        private CharacterInfo _MasterCharacter;

        public MasterType MasterType
        {
            get { return _MasterType; }
            set
            {
                if (_MasterType == value) return;

                var oldValue = _MasterType;
                _MasterType = value;

                OnChanged(oldValue, value, "MasterType");
            }
        }
        private MasterType _MasterType;

        public DateTime MasterTime
        {
            get { return _MasterTime; }
            set
            {
                if (_MasterTime == value) return;

                var oldValue = _MasterTime;
                _MasterTime = value;

                OnChanged(oldValue, value, "MasterTime");
            }
        }
        private DateTime _MasterTime;

        public decimal MasterExperience
        {
            get { return _MasterExperience; }
            set
            {
                if (_MasterExperience == value) return;

                var oldValue = _MasterExperience;
                _MasterExperience = value;

                OnChanged(oldValue, value, "MasterExperience");
            }
        }
        private decimal _MasterExperience;

        public int MasterTotalCount
        {
            get { return _MasterTotalCount; }
            set
            {
                if (_MasterTotalCount == value) return;

                var oldValue = _MasterTotalCount;
                _MasterTotalCount = value;

                OnChanged(oldValue, value, "MasterTotalCount");
            }
        }
        private int _MasterTotalCount;


        protected override void OnDeleted()
        {
            Account = null;
            Companion = null;
            Partner = null;
            MasterCharacter = null;
            
            base.OnDeleted();
        }




        public PlayerObject Player;
        public LinkedListNode<CharacterInfo> RankingNode;

        public SelectInfo ToSelectInfo()
        {
            return new SelectInfo
            {
                CharacterIndex = Index,
                CharacterName = CharacterName,
                Class = Class,
                Gender = Gender,
                Level = Level,
                Location = CurrentMap?.Index ?? 0,
                LastLogin = LastLogin,
            };
        }

        protected override void OnCreated()
        {
            base.OnCreated();

            LastStats = new Stats();
            HermitStats = new Stats();

            Observable = true;
        }

        /*  protected override void OnLoaded()
          {
              base.OnLoaded();

              if (LastStats == null)
                  LastStats = new Stats();

              if (HermitStats == null)
                  HermitStats = new Stats();
          }*/

        public override string ToString()
        {
            return CharacterName;
        }
    }
}
