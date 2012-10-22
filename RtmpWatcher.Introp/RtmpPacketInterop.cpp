#include "stdafx.h"

#include "RtmpPacketInterop.h"
#include "RtmpPacket.h"

using namespace System;
using namespace System::Runtime::InteropServices;

RtmpInterop::RtmpPacketInterop::RtmpPacketInterop(RtmpPacket * packet){
	_bytes = gcnew array<unsigned char>(packet->dataLength);

	Marshal::Copy(IntPtr(const_cast<void*>(static_cast<const void*>(packet->data))), _bytes, 0, packet->dataLength);

	_length = packet->dataLength;

	_sourceIP = gcnew String(packet->sourceIp);

	_destIp = gcnew String(packet->destIp);

	_packetType = DeterminePacketType(packet->rtmpPacketType);
}

RtmpInterop::RtmpPacketTypeManaged::RtmpPacketType RtmpInterop::RtmpPacketInterop::GetRtmpPacketType(){
	return _packetType;
}

array<unsigned char>^ RtmpInterop::RtmpPacketInterop::GetBytes(){
	return _bytes;
}

String ^ RtmpInterop::RtmpPacketInterop::GetSourceIP(){
	return _sourceIP;
}

String ^ RtmpInterop::RtmpPacketInterop::GetDestIP(){
	return _destIp;
}

int RtmpInterop::RtmpPacketInterop::GetLength(){
	return _length;
}

RtmpInterop::RtmpPacketTypeManaged::RtmpPacketType RtmpInterop::RtmpPacketInterop::DeterminePacketType(RtmpPacket::RtmpDataTypes rawType){
	switch(rawType){
		case RtmpPacket::RtmpDataTypes::Handshake: 
			return RtmpPacketTypeManaged::RtmpPacketType::Handshake;

		case RtmpPacket::RtmpDataTypes::ChunkSize:
			return RtmpPacketTypeManaged::RtmpPacketType::ChunkSize; 
		
		case RtmpPacket::RtmpDataTypes::Ping:
			return RtmpPacketTypeManaged::RtmpPacketType::Ping; 
		
		case RtmpPacket::RtmpDataTypes::ServerBandwidth:
			return RtmpPacketTypeManaged::RtmpPacketType::ServerBandwidth; 
		
		case RtmpPacket::RtmpDataTypes::ClientBandwidth:
			return RtmpPacketTypeManaged::RtmpPacketType::ClientBandwidth; 
		
		case RtmpPacket::RtmpDataTypes::Audio:
			return RtmpPacketTypeManaged::RtmpPacketType::Audio; 
		
		case RtmpPacket::RtmpDataTypes::Video:
			return RtmpPacketTypeManaged::RtmpPacketType::Video; 
		
		case RtmpPacket::RtmpDataTypes::Notify:
			return RtmpPacketTypeManaged::RtmpPacketType::Notify; 
		
		case RtmpPacket::RtmpDataTypes::Invoke:
			return RtmpPacketTypeManaged::RtmpPacketType::Invoke; 
		
		case RtmpPacket::RtmpDataTypes::AggregateMessage:
			return RtmpPacketTypeManaged::RtmpPacketType::AggregateMessage; 
	}
	return RtmpPacketTypeManaged::RtmpPacketType::Unknown;
}