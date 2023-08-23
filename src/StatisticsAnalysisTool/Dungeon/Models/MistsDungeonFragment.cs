﻿using StatisticsAnalysisTool.Cluster;
using StatisticsAnalysisTool.Common;
using System;
using ValueType = StatisticsAnalysisTool.Enumerations.ValueType;

namespace StatisticsAnalysisTool.Dungeon.Models;

public class MistsDungeonFragment : DungeonBaseFragment
{
    private double _might;
    private double _favor;
    private double _mightPerHour;
    private double _favorPerHour;

    public MistsDungeonFragment(Guid guid, MapType mapType, DungeonMode mode, string mainMapIndex) : base(guid, mapType, mode, mainMapIndex)
    {
    }

    public MistsDungeonFragment(DungeonDto dto) : base(dto)
    {
        Might = dto.Might;
        Favor = dto.Favor;
    }

    public double Might
    {
        get => _might;
        set
        {
            _might = value;
            MightPerHour = value.GetValuePerHour(TotalRunTimeInSeconds <= 0 ? (DateTime.UtcNow - EnterDungeonFirstTime).Seconds : TotalRunTimeInSeconds);
            OnPropertyChanged();
        }
    }

    public double Favor
    {
        get => _favor;
        set
        {
            _favor = value;
            FavorPerHour = value.GetValuePerHour(TotalRunTimeInSeconds <= 0 ? (DateTime.UtcNow - EnterDungeonFirstTime).Seconds : TotalRunTimeInSeconds);
            OnPropertyChanged();
        }
    }

    #region Composite values that are not in the DTO

    public double MightPerHour
    {
        get => double.IsNaN(_mightPerHour) ? 0 : _mightPerHour;
        private set
        {
            _mightPerHour = value;
            OnPropertyChanged();
        }
    }

    public double FavorPerHour
    {
        get => double.IsNaN(_favorPerHour) ? 0 : _favorPerHour;
        private set
        {
            _favorPerHour = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public void Add(double value, ValueType type)
    {
        switch (type)
        {
            case ValueType.Fame:
                Fame += value;
                return;
            case ValueType.ReSpec:
                ReSpec += value;
                return;
            case ValueType.Silver:
                Silver += value;
                return;
            case ValueType.Might:
                Might += value;
                return;
            case ValueType.Favor:
                Favor += value;
                return;
        }
    }
}